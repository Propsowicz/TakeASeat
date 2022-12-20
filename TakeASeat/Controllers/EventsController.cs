using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

using System.Linq;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using TakeASeat.Services;
using TakeASeat.Services.Generic;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IEventRepository _eventRepo;

        public EventsController(IUnitOfWork unitOfWork, 
            IMapper mapper, DatabaseContext context, IEventRepository eventRepo
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
            _eventRepo = eventRepo;   
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllWtihDetails([FromQuery] RequestParams r_params)
        {
            // ordering needs to be done

            var query = await _eventRepo.GetPaginatedAllWithoutPastShowsOrderAsc(r_params, orderBy: q => q.EventType.Name);
            var response = _mapper.Map<List<GetEventDetailsDTO>>(query);

            return StatusCode(200, response);
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

        [HttpGet("eloszka")]
        public async Task<IActionResult> GetSomeTest([FromQuery] RequestParams r_params)
        {

            //var response = await _ev.GetAll();
            return Ok(await _eventRepo.GetPaginatedAllWithoutPastShowsOrderAsc(r_params, orderBy: q => q.EventType.Name));
        }
    }
}
