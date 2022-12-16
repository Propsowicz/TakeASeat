using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using TakeASeat.Data.DatabaseContext;
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
        private readonly DatabaseContext _context;

        public EventsController(IUnitOfWork unitOfWork, IMapper mapper, DatabaseContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PaginatedGetAll([FromQuery] RequestParams r_params)
        {
            var typesList = _context.EventTypes
                            .Select(q => q.Name)
                            .ToList();

            if (r_params.FilterString != null)
            {
                typesList = r_params.FilterString;
            }

            var tagIdList = _context.EventTagEventM2M
                            .Where(t => t.EventTag.TagName
                            .Equals(r_params.SearchString))
                            .Select(t => t.EventId)
                            .ToList();

            var events = await _unitOfWork.Events.PaginatedGetAll(includes: new List<string> { "EventTags", "EventType", "Creator", "Shows" },
                requestParams: r_params, expression: ev => ev.Name.Contains(r_params.SearchString)
                                                        || tagIdList.Contains(ev.Id)
                                                        && typesList.Contains(ev.EventType.Name));

            var response = _mapper.Map<List<GetEventDetailsDTO>>(events);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSingleEvent(int id)
        {
            var event_ = await _unitOfWork.Events.Get(ev => ev.Id == id, includes: new List<string> { "EventTags", "EventType", "Creator", "Shows" }); 
            var response = _mapper.Map<GetEventDetailsDTO>(event_);

            return Ok(response);
        }
    }
}
