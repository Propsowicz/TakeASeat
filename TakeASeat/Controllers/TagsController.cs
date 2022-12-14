using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Data;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using TakeASeat.Services.EventTagRepository;
using TakeASeat.Services.ShowService;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IShowRepository _showRepository;
        private readonly IEventTagRepository _eventTagRepository;
        private readonly IMapper _mapper;
        public TagsController(IShowRepository showRepository, IMapper mapper, IEventTagRepository eventTagRepository)
        {
            _showRepository= showRepository;
            _eventTagRepository= eventTagRepository;
            _mapper= mapper;
        }
               

        [HttpGet("tags-list")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getEventTags()
        {
            var query = await _eventTagRepository.getEventTags();
            var response = _mapper.Map<IList<GetEventTagDTO>>(query);

            return StatusCode(200, response);
        }

        [HttpPost("add-multiple")]  //maybe del?
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddEventTags([FromBody] List<GetEventTagDTO> eventTagsDTO, int eventId)
        {
            if (!ModelState.IsValid || eventId < 1)
            {
                return BadRequest();
            }
            await _eventTagRepository.AddEventTags(eventTagsDTO, eventId);

            return StatusCode(200);
        }

    }
}
