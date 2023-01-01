using AutoMapper;
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

        public ShowsController(IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _showRepository = serviceProvider.GetRequiredService<IShowRepository>();
        }

        [HttpGet("home-page")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetShowsHomePage()                  // USED
        {
            int PageNumber = 1;
            int PageSize = 5;
            var query = await _showRepository.GetShows(PageNumber, PageSize);

            var response = _mapper.Map<IList<GetClosestShows>>(query);

            return StatusCode(200, response);
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetShows([FromQuery] RequestShowParams requestParams)                  // USED
        {            
            var query = await _showRepository.GetShows(requestParams.PageNumber, requestParams.PageSize);

            var response = _mapper.Map<IList<GetClosestShows>>(query);

            return StatusCode(200, response);
        }

        [HttpGet("records-number")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetShowRecordNumber()                  // USED
        {
            var query = await _showRepository.GetShowRecordNumber();

            return StatusCode(200, query);
        }

        [HttpGet("{id:int}")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSingleShow(int id)                      // USED
        {
            var query = await _showRepository.GetShowDetails(id);

            var response = _mapper.Map<GetClosestShows>(query);
            return StatusCode(200, response);
        }
    }
}
