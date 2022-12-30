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
        public async Task<IActionResult> getPaymentData([FromQuery] RequestPaymentParams requestPaymentParams)
        {
            var response = await _paymentRepository.getPaymentData("e17202bb-0183-40db-8ef5-1811013e075d");
            //var response = await _paymentRepository.getPaymentData(requestPaymentParams.UserId);
            return StatusCode(200, response);
        }

        

        
    }
}
