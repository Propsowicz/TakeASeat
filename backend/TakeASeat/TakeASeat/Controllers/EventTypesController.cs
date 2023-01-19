using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Models;
using TakeASeat.Services.EventTypesService;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTypesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEventTypeRepository _eventTypeRepository;
        public EventTypesController(IMapper mapper, IEventTypeRepository eventTypeRepository)
        {
            _mapper= mapper;
            _eventTypeRepository= eventTypeRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEventTypes()
        {
            var query = await _eventTypeRepository.getEventTypes();
            var response = _mapper.Map<List<GetEventTypeDTO>>(query);

            return StatusCode(200, response);
        }
    }
}
