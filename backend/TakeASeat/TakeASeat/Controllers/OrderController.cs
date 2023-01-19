using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using TakeASeat.Services.PaymentService;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;

        public OrderController(IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _paymentRepository = serviceProvider.GetRequiredService<IPaymentRepository>();
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator,Organizer,User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getBuyedItems([FromQuery] RequestPaymentParams requestPaymentParams)
        {
            var query = await _paymentRepository.getReservedSeats(requestPaymentParams.UserId);
            var response = _mapper.Map<IList<GetReservedSeatsDTO>>(query);

            return StatusCode(200, response);
        }

    }
}
