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
        private readonly IMapper _mapper;
        private readonly ISeatRepository _seatRepository;
        private readonly IShowRepository _showRepository;
        private readonly ISeatResRepository _seatReservationRepository;

        public SeatsController(IMapper mapper, ISeatRepository seatRepository, IShowRepository showRepository, ISeatResRepository seatReservationRepository)
        {
            _mapper = mapper;
            _seatRepository = seatRepository;
            _showRepository = showRepository;
            _seatReservationRepository = seatReservationRepository;
        }


        [HttpGet("ShowId-{showId:int}")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSeats(int showId)                               // USED
        {
            if (showId < 1)
            {
                return StatusCode(400);
            }
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
        public async Task<IActionResult> CreateMultipleSeats([FromBody] IEnumerable<CreateSeatDTO> seatsDTO)    
        {
            if (!ModelState.IsValid || seatsDTO.Count() < 1)
            {
                return StatusCode(400);
            }
            var showId = seatsDTO.FirstOrDefault().ShowId;
            if (showId < 1)
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
            if (!ModelState.IsValid || seatParams.SeatId < 1)
            {
                return StatusCode(400);
            }
            await _seatReservationRepository.RemoveSingleSeatFromOrder(seatParams.SeatId);

            return StatusCode(204);
        }
        

    }
}
