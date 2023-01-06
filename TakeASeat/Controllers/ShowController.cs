using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Models;
using TakeASeat.Services.ShowService;
using Microsoft.AspNetCore.Authorization;


namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IShowRepository _showRepository;

        public ShowController(IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _showRepository = serviceProvider.GetRequiredService<IShowRepository>();
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

        [HttpPost("create")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator,Organizer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateShow(CreateShowDTO showDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _showRepository.CreateShow(showDTO);

            return StatusCode(201);
        }
    }
}
