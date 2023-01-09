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
using TakeASeat.Services.Generic;
using TakeASeat.Services.EventService;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public EventsController( 
            IMapper mapper, IEventRepository eventRepository
            )
        {
            _mapper = mapper;
            _eventRepository = eventRepository;   
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEvents([FromQuery] RequestEventParams requestParams)
        {
            var query = await _eventRepository.GetEvents(requestParams);
            var response = _mapper.Map<List<GetEventDTO>>(query);

            return StatusCode(200, response);
        }

        [HttpGet("by-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEventsByUser([FromQuery] RequestEventParams requestParams, string userName)
        {
            if (userName == "" || userName == null)
            {
                return StatusCode(404);
            }

            var query = await _eventRepository.GetEventsByUser(requestParams, userName);
            var response = _mapper.Map<List<GetEventWithListOfShowsDTO>>(query);

            return StatusCode(200, response);
        }


        [HttpGet("records-number")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEventRecordsNumber()
        {
            var response = await _eventRepository.GetEventRecordsNumber();
            
            return StatusCode(200, response);
        }              
    }
}
