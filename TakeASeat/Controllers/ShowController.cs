﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Models;
using TakeASeat.Services.Generic;
using TakeASeat.Services.ShowService;
using X.PagedList;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly IShowRepository _showRepository;

        public ShowController(IServiceProvider serviceProvider)
        {
            _unitOfWork= serviceProvider.GetRequiredService<IUnitOfWork>();
            _mapper= serviceProvider.GetRequiredService<IMapper>();
            _showRepository = serviceProvider.GetRequiredService<IShowRepository>();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClosestShowsHomePage()                  // USED
        {
            var query = await _showRepository.GetClosestShows();

            var response = _mapper.Map<IList<GetClosestShows>>(query);

            return StatusCode(200, response);
        }

        [HttpGet("{id:int}")]                                                       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSingleShow(int id)                      // USED
        {
            var query = await _showRepository.GetShowDetails();

            var response = _mapper.Map<GetClosestShows>(query);
            return StatusCode(200, response);
        }

    }
}
