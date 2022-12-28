using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using TakeASeat.Services.PaymentService;
using Microsoft.EntityFrameworkCore;


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
        public async Task<IActionResult> getBuyedItems([FromQuery] RequestPaymentParams requestPaymentParams)
        {            
            var query = await _paymentRepository.getSeatReservations(requestPaymentParams.UserId);
            var response = _mapper.Map<IList<GetSeatReservationDTO>>(query);

            return StatusCode(200, response);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> deleteSeatReservation([FromBody] RequestPaymentDeleteParams requestPaymentDeleteParams)
        {
            await _paymentRepository.removeSeatReservations(requestPaymentDeleteParams.SeatReservationId);

            return StatusCode(204);
        }
    }
}
