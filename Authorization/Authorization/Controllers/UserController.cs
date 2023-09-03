using Authorization.Models;
using Authorization.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJWTService _jwtService;
        public UserController(IJWTService jwtService)
        {
            _jwtService = jwtService;
        }
        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserCredentials user)
        {
            if (!_jwtService.Authen(user))
            {
                return NotFound(" Such user doesn't exist yet! ");
            }
            return Ok(_jwtService.CreateToken(user));
        }
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "GetUser")]
        public IActionResult GetUsers()
        {
            return Ok(UserModel.Users);
        }

        [HttpGet("users")]
        [Authorize]
        public IActionResult GetUser(int id)
        {
            return Ok("Hello this is secured method");
        }

    }
}
