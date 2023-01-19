using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using TakeASeat.Services.PaymentService;
using TakeASeat.Data;
using Microsoft.AspNetCore.Authorization;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IServiceProvider serviceProvider)
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
        public async Task<IActionResult> GetPaymentData([FromQuery] RequestPaymentParams requestPaymentParams)
        {
            var response = await _paymentRepository.getPaymentData(requestPaymentParams.UserId);
            return StatusCode(200, response);
        }

        [HttpGet("total-user-cost")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator,Organizer,User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTotalCostByUser([FromQuery] RequestPaymentParams requestPaymentParams)
        {
            var response = await _paymentRepository.getTotalCost(requestPaymentParams.UserId);
            return StatusCode(200, response);
        }

        [HttpPost("create")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator,Organizer,User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePaymentTransactionRecord([FromBody] CreatePaymentTranscationDTO paymentTranscationDTO)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }

            var query = _mapper.Map<PaymentTransaction>(paymentTranscationDTO);
            await _paymentRepository.createPaymentTransactionRecord(query);

            return StatusCode(204);
        }           
    }
}
