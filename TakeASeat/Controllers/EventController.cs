using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Models;
using TakeASeat.Services.EventService;
using TakeASeat.Services.Generic;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IEventRepository _eventRepo;

        public EventController(IUnitOfWork unitOfWork,
            IMapper mapper, DatabaseContext context, IEventRepository eventRepo
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
            _eventRepo = eventRepo;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSingleEvent(int id)
        {
            var event_ = await _unitOfWork.Events.Get(ev => ev.Id == id, includes: new List<string> { "EventTags", "EventType", "Creator", "Shows" });
            var response = _mapper.Map<GetEventDetailsDTO>(event_);

            return StatusCode(200, response);
        }

        [HttpPost("create-with-tags")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventWithTagsDTO eventData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _eventRepo.CreateEventWithTags(eventData.eventDTO, eventData.eventTagsDTO);

            return StatusCode(201);
        }
    }
}
