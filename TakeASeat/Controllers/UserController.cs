using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using TakeASeat.Data;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using TakeASeat.Services.UserService;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;
        private readonly IUserRepository _userRepository;

        public UserController(UserManager<User> userManager, IMapper mapper, IAuthManager authManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authManager = authManager;
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO userDTO)
        {
            if (!ModelState.IsValid || userDTO.UserName.IsNullOrEmpty() || userDTO.Password.IsNullOrEmpty())
            {
                return StatusCode(400);
            }
            var user = _mapper.Map<User>(userDTO);
            var response = await _userManager.CreateAsync(user, userDTO.Password);
                        
            await _userRepository.AddToRoleNamedUser(user);
            return StatusCode(201);             
        }

        [HttpPost("refresh")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken([FromBody] JWTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response = await _authManager.VerifyRefreshToken(request);

            if (response ==null) { return StatusCode(400); }
            else { return StatusCode(200, response); }

        }

        [HttpPost("login")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDTO userDTO)
        {
            //throw new Exception();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }            

            if (!await _authManager.ValidateUser(userDTO))
            {
                return Unauthorized();
            }
            return Accepted(new {AccessToken = await _authManager.CreateAccessJWToken(userDTO), RefreshToken = await _authManager.CreateRefreshJWToken(userDTO)});
        }
    }
}
