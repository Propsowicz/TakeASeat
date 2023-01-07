using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetRoles()
        {
            var response = await _userRepository.GetRoles();

            return StatusCode(200, response);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _userRepository.GetUsers();

            return StatusCode(200, response);
        }

    }
}
