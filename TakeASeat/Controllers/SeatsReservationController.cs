using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Data;
using TakeASeat.RequestUtils;
using TakeASeat.Services.Generic;
using TakeASeat.Services.SeatReservationService;
using TakeASeat.Services.SeatService;
using TakeASeat.Services.ShowService;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsReservationController : ControllerBase
    {
        private readonly IMapper _mapper;        
        private readonly ISeatResRepository _seatResRepository;

        public SeatsReservationController(IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _seatResRepository = serviceProvider.GetRequiredService<ISeatResRepository>();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReserveSeats([FromBody] RequestOrderParams rParams)
        {

            var seats = _mapper.Map<IEnumerable<Seat>>(rParams.Seats);

            await _seatResRepository.CreateSeatReservations(rParams.BuyerId, rParams.EventId, seats);


            return StatusCode(202);
        }
    }
}
