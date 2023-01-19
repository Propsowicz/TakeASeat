using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Data;
using TakeASeat.RequestUtils;
using TakeASeat.Services.SeatReservationService;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsReservationController : ControllerBase
    {
        private readonly IMapper _mapper;        
        private readonly ISeatResRepository _seatResRepository;        

        public SeatsReservationController(IMapper mapper, ISeatResRepository seatResRepository)
        {
            _mapper = mapper;
            _seatResRepository = seatResRepository;           
        }
        

        [HttpPost("create")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator,Organizer,User")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReserveSeats([FromBody] RequestOrderParams rParams)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }
            var seats = _mapper.Map<IList<Seat>>(rParams.Seats);
            await _seatResRepository.CreateSeatReservation(rParams.UserId, seats);
            return StatusCode(202);
        }

        [HttpPost("delete")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator,Organizer,User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSeatReservation([FromBody] RequestReservationParams seatReservation)
        {
            if (!ModelState.IsValid || seatReservation.seatReservationId < 1)
            {
                return StatusCode(400);
            }

            await _seatResRepository.DeleteSeatReservation(seatReservation.seatReservationId);

            return StatusCode(204);
        }
    }
}
