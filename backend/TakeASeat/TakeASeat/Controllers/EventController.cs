using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using TakeASeat.Services.EventService;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public EventController(IMapper mapper, IEventRepository eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        [HttpGet("{eventId:int}")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEvent(int eventId)
        {
            if (eventId < 1) 
            {
                return StatusCode(400);
            }
            var query = await _eventRepository.getEvent(eventId);
            var response = _mapper.Map<GetEventDetailsDTO>(query);

            return StatusCode(200, response);
        }

        [HttpPost("create-with-tags")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator,Organizer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventWithTagsDTO eventData)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }

            await _eventRepository.createEventWithTags(eventData.eventDTO, eventData.eventTagsDTO);

            return StatusCode(201);
        }

        [HttpPost("edit-with-tags")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator,Organizer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditEvent([FromBody] EditEventWithTagsDTO eventData)
        {
            if (!ModelState.IsValid || eventData.eventDTO.Id < 1)
            {
                return StatusCode(400);
            }

            await _eventRepository.editEventWithTags(eventData.eventDTO, eventData.eventTagsDTO);

            return StatusCode(200);
        }

        [HttpPost("delete")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator,Organizer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEvent([FromBody] RequestEventDeleteParams requestParams)
        {
            if (!ModelState.IsValid || requestParams.EventId < 1)
            {
                return StatusCode(400);
            }
            await _eventRepository.deleteEvent(requestParams.EventId);            

            return StatusCode(200);
        }
    }
}
