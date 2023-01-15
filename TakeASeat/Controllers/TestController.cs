using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Services.TicketService;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {   
        private readonly ITicketRepository _ticketRepository;
        public TestController(ITicketRepository ticketRepository)
        {
            _ticketRepository= ticketRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //await _ticketRepository.SendTicketsViaEmail();

            return Ok();
        }

        //[HttpGet("pdf")]
        //public async Task<IActionResult> GetPDF()
        //{
        //    var response = await _ticketRepository.CreateRangeOfTicketRecords(new List<int> () { 1,2,3});

        //    return response;
        //}
    }
}
