using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Models;
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
    }
}
