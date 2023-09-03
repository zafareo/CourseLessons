using Application.MediatrEntities.Users.Commands;
using Application.MediatrEntities.Users.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GapKo_p.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiBaseController
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserCommand command)
        {
            await _mediatr.Send(command);
            return View("CreateUserView");
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> DeleteUser([FromForm] DeleteUserCommand command)
        {
            await _mediatr.Send(command);
            return View("DeleteUserView");
        }


        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserCommand command)
        {
            await _mediatr.Send(command);
            return View("UpdateUserView");
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUser()
        {
            await _mediatr.Send(new GetAllUsersQuery());
            return View("GetAllUsersView");
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetByIdUser([FromQuery] GetByIdUserQuery command)
        {
            await _mediatr.Send(command);
            return View("GetByIdUserView");
        }
    }
}
