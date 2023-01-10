using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Data;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using TakeASeat.Services.Generic;
using TakeASeat.Services.SeatReservationService;
using TakeASeat.Services.SeatService;
using TakeASeat.Services.ShowService;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeatRepository _seatRepository;
        private readonly IShowRepository _showRepository;
        private readonly ISeatResRepository _seatResRepository;

        public SeatsController(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _seatRepository = serviceProvider.GetRequiredService<ISeatRepository>();
            _showRepository = serviceProvider.GetRequiredService<IShowRepository>();
            _seatResRepository = serviceProvider.GetRequiredService<ISeatResRepository>();
        }


        [HttpGet("ShowId-{showId:int}")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSeats(int showId)                               // USED
        {
            var seats = await _seatRepository.GetSeats(showId);
            var response = _mapper.Map<IEnumerable<GetSeatDTO[]>>(seats);
            return StatusCode(200, response);
        }

        [HttpPost("create-multiple")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator,Organizer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMultipleSeats([FromBody] IEnumerable<CreateSeatDTO> seatsDTO)    // USED
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }
            var showId = seatsDTO.FirstOrDefault().ShowId;
            if (showId == null)
            {
                return StatusCode(400);
            }
            await _showRepository.SetShowReadyToSell(showId);

            var seats = _mapper.Map<IEnumerable<Seat>>(seatsDTO);
            await _seatRepository.CreateMultipleSeats(seats);
            return StatusCode(201);
        }

        [HttpPost("remove-reservation")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator,Organizer,User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveSeatReservation([FromBody] RequestSeatParams seatParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _seatRepository.RemoveSingleReservation(seatParams.SeatId);

            return StatusCode(204);
        }
        

    }
}
