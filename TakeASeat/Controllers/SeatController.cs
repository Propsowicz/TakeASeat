using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Data;
using TakeASeat.IRepository;
using TakeASeat.Models;

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
            _unitOfWork= unitOfWork;
            _mapper= mapper;
        }

        [HttpPost("create-multiple")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSeat([FromBody] IEnumerable<CreateSeatDTO> seatsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var seats = _mapper.Map<IEnumerable<Seat>>(seatsDTO);
            await _unitOfWork.Seats.CreateRange(seats);
            await _unitOfWork.Save();

            return Ok();
        }

    }
}
