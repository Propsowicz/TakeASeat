﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Data;
using TakeASeat.IRepository;
using TakeASeat.Models;
using TakeASeat.Repository;
using TakeASeat.UserServices;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public UserController(UserManager<User> userManager, IMapper mapper, IAuthManager authManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authManager = authManager;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = _mapper.Map<User>(userDTO);
            var response = await _userManager.CreateAsync(user, userDTO.Password);

            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _authManager.ValidateUser(userDTO))
            {
                return Unauthorized();
            }
            return Accepted(new {AccessToken = await _authManager.CreateJWToken(userDTO)});

        }
    }
}