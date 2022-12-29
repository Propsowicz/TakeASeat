using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> getBuyedItems([FromQuery] RequestPaymentParams requestPaymentParams)
        {
            var query = await _paymentRepository.getReservedSeats(requestPaymentParams.UserId);
            var response = _mapper.Map<IList<GetReservedSeatsDTO>>(query);

            return StatusCode(200, response);
        }

    }
}
