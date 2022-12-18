using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Data;
using TakeASeat.IRepository;
using TakeASeat.Models;
using TakeASeat.BackgroundServices;
using Azure;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SeatController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("multiple-creation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSeats([FromBody] IEnumerable<CreateSeatDTO> seatsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var seats = _mapper.Map<IEnumerable<Seat>>(seatsDTO);
            await _unitOfWork.Seats.CreateRange(seats);
            await _unitOfWork.Save();

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
