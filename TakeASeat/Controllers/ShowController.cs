using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Models;
using TakeASeat.Services.ShowService;
using Microsoft.AspNetCore.Authorization;
using TakeASeat.RequestUtils;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IShowRepository _showRepository;

        public ShowController(IMapper mapper, IShowRepository showRepository)
        {
            _mapper = mapper;
            _showRepository = showRepository;
        }

        [HttpGet("{id:int}")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetShow(int id)                      
        {
            if (id < 1)
            {
                return StatusCode(400);
            }
            var query = await _showRepository.GetShowDetails(id);

            var response = _mapper.Map<GetClosestShows>(query);
            return StatusCode(200, response);
        }

        [HttpPost("create")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator,Organizer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateShow([FromBody] CreateShowDTO showDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _showRepository.CreateShow(showDTO);

            return StatusCode(201);
        }

        [HttpPost("delete")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator,Organizer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteShow([FromBody] DeleteShowParams requestParams)
        {
            if (requestParams.ShowId < 1)
            {
                return StatusCode(400);
            }
            await _showRepository.DeleteShow(requestParams.ShowId);

            return StatusCode(200);
        }
    }
}
