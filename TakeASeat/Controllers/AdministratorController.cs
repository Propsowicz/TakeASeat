using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using TakeASeat.Services.UserService;

namespace TakeASeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AdministratorController(IUserRepository userRepository)
        {
            _userRepository= userRepository;
        }

        [HttpGet("roles")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoles()
        {
            var response = await _userRepository.GetRoles();

            return StatusCode(200, response);
        }

        [HttpGet("users")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsers([FromQuery] RequestUserParams requestParams)
        {
            var response = await _userRepository.GetUsers(requestParams);

            return StatusCode(200, response);
        }

        [HttpGet("records-number")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserRecordsNumber()
        {
            var response = await _userRepository.GetUsersRecordsNumber();

            return StatusCode(200, new {recordsNumber = response});
        }

        [HttpPost("edit-roles")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeUserRoles([FromBody] EditUserRolesDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _userRepository.ChangeRoles(userDTO);

            return StatusCode(200);
        }

    }
}
