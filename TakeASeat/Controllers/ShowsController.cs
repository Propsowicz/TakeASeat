using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using TakeASeat.Services.Generic;
using TakeASeat.Services.ShowService;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IShowRepository _showRepository;

        public ShowsController(IMapper mapper, IShowRepository showRepository)
        {
            _mapper = mapper;
            _showRepository = showRepository;
        }

        [HttpGet("home-page")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetShowsToHomePage()                  
        {
            int PageNumber = 1;
            int PageSize = 5;
            var response = await _showRepository.GetShows(PageNumber, PageSize);

            return StatusCode(200, response);
        }
                
        [HttpGet]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetShows([FromQuery] RequestShowParams requestParams)                  
        {            
            var response = await _showRepository.GetShows(requestParams.PageNumber, requestParams.PageSize);

            return StatusCode(200, response);
        }

        [HttpGet("by-tags")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetShowsByEventTags([FromQuery] RequestTagsParams requestParams)
        {
            var response = await _showRepository.GetShowsByEventTag(requestParams);

            return StatusCode(200, response);
        }

        [HttpGet("records-number")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetShowsRecordNumber()                  
        {
            var query = await _showRepository.GetShowRecordNumber();

            return StatusCode(200, query);
        }

        [HttpGet("eventId-{eventId:int}")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetShowsByEvent(int eventId)                  
        {
            if (eventId < 1)
            {
                return StatusCode(400);
            }
            var response = await _showRepository.GetShowsByEvent(eventId);

            return StatusCode(200, response);
        }

        
    }
}
