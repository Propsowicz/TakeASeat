﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.RequestUtils;
using TakeASeat.Services.BackgroundService;
using TakeASeat.Services.Generic;
using TakeASeat.Services.SeatReservationService;
using TakeASeat.Services.SeatService;
using TakeASeat.Services.ShowService;
using X.PagedList;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsReservationController : ControllerBase
    {
        private readonly IMapper _mapper;        
        private readonly ISeatResRepository _seatResRepository;
        private readonly IReleaseReservationService _releaseResRepository;
        private readonly DatabaseContext _context;

        public SeatsReservationController(IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _seatResRepository = serviceProvider.GetRequiredService<ISeatResRepository>();
            _releaseResRepository = serviceProvider.GetRequiredService<IReleaseReservationService>();
            _context = serviceProvider.GetRequiredService<DatabaseContext>();
            
        }
        

        [HttpPost("create")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator,Organizer,User")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReserveSeats([FromBody] RequestOrderParams rParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var seats = _mapper.Map<IList<Seat>>(rParams.Seats);
            await _seatResRepository.CreateSeatReservation(rParams.UserId, seats);
            return StatusCode(202);
        }

        [HttpPost("delete")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator,Organizer,User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> deleteSeatReservation([FromBody] RequestReservationParams seatReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _seatResRepository.DeleteSeatReservation(seatReservation.seatReservationId);

            return StatusCode(204);
        }
    }
}
