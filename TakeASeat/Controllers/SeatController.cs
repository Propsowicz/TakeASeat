using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Data;
using TakeASeat.Models;
using TakeASeat.BackgroundServices;
using Azure;
using TakeASeat.Services.Generic;
using TakeASeat.Services.SeatService;
using TakeASeat.Services.ShowService;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeatRepository _seatRepository;
        private readonly IShowRepository _showRepository;

        public SeatController(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _seatRepository = serviceProvider.GetRequiredService<ISeatRepository>();
            _showRepository = serviceProvider.GetRequiredService<IShowRepository>();
        }
        [HttpGet("ShowId-{showId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSeats(int showId)                               // USED
        {
            var seats = await _seatRepository.GetSeats(showId);
            //var response = _mapper.Map<IList<GetSeatDTO>>(seats);
            return StatusCode(200, seats);
        }

        [HttpPost("create-multiple")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMultipleSeats([FromBody] IEnumerable<CreateSeatDTO> seatsDTO)    // USED
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var showId = seatsDTO.FirstOrDefault().ShowId;
            await _showRepository.SetShowReadyToSell(showId);

            var seats = _mapper.Map<IEnumerable<Seat>>(seatsDTO);
            await _seatRepository.CreateMultipleSeats(seats);
            return StatusCode(201);
        }

        [HttpPut("reservation")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReserveSeats([FromBody] IEnumerable<ReserveSeatsDTO> seatsDTO)
        {            
            foreach (var seat in seatsDTO)
            {
                if (!ModelState.IsValid || seat.Id < 1)
                {
                    return BadRequest();
                }

                var seatToUpdate = await _unitOfWork.Seats.Get(src => src.Id == seat.Id);
                if (seatToUpdate == null)
                {
                    return BadRequest();
                }
                _mapper.Map(seat, seatToUpdate);
                _unitOfWork.Seats.Update(seatToUpdate);
                await _unitOfWork.Save();
            }
            return StatusCode(200);
        }

    }
}
