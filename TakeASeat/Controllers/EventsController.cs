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
        public async Task<IActionResult> GetEvents([FromQuery] RequestEventParams requestParams)
        {
            var query = await _eventRepo.GetEvents(requestParams);
            var response = _mapper.Map<List<GetEventDTO>>(query);

            return StatusCode(200, response);
        }

        [HttpGet("by-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEventsByUser([FromQuery] RequestEventParams requestParams, string userName)
        {
            var query = await _eventRepo.GetEventsByUser(requestParams, userName);
            var response = _mapper.Map<List<GetEventDTO>>(query);

            return StatusCode(200, response);
        }


        [HttpGet("records-number")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEventRecordsNumber()
        {
            var response = await _eventRepo.GetEventRecordsNumber();
            
            return StatusCode(200, response);
        }

        

    }
}
