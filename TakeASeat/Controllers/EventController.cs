using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
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
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSingleEvent(int id)
        {
            var query = await _eventRepo.GetEvent(id);
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
                return BadRequest();
            }

            await _eventRepo.CreateEventWithTags(eventData.eventDTO, eventData.eventTagsDTO);

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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _eventRepo.EditEventWithTags(eventData.eventDTO, eventData.eventTagsDTO);

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
            await _eventRepo.DeleteEvent(requestParams.EventId);            

            return StatusCode(200);
        }
    }
}
