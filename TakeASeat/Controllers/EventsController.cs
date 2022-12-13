using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.IRepository;
using TakeASeat.Models;
using TakeASeat.RequestUtils;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PaginatedGetAll([FromQuery] RequestParams r_params)
        {
            var events = await _unitOfWork.Events.PaginatedGetAll(requestParams: r_params);
            var response = _mapper.Map<List<GetEventDTO>>(events);
            return Ok(response);
        }
    }
}
