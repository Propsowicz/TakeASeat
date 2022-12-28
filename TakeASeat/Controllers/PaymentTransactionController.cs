using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TakeASeat.RequestUtils;
using TakeASeat.Services.PaymentService;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTransactionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentTransactionController(IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _paymentRepository = serviceProvider.GetRequiredService<IPaymentRepository>();
        }


        [HttpGet]
        public async Task<IActionResult> getPaymentData([FromQuery] RequestPaymentParams requestPaymentParams)
        {

            //var response = _paymentRepository.getPaymentData("e17202bb-0183-40db-8ef5-1811013e075d");
            var response = _paymentRepository.getPaymentData(requestPaymentParams.UserId);
            return StatusCode(200, response);
        }
    }
}
