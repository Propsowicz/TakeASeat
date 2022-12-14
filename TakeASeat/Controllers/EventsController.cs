using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
            var events = await _unitOfWork.Events.PaginatedGetAll(requestParams: r_params, expression: ev => ev.Name.Contains(r_params.SearchString)
                                                                                                            && r_params.FilterString.Contains(ev.EventType.Name));
            var response = _mapper.Map<List<GetEventDTO>>(events);
            return Ok(response);
        }
    }
}
