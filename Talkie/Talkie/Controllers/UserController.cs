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
        => Ok(await _mediatr.Send(command));

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> DeleteUser([FromForm] DeleteUserCommand command)
            => Ok(await _mediatr.Send(command));


        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserCommand command)
            => Ok(await _mediatr.Send(command));


        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUser()
            => Ok(await _mediatr.Send(new GetAllUsersQuery()));

        [HttpGet("getById")]
        public async Task<IActionResult> GetByIdUser([FromQuery] GetByIdUserQuery command)
            => Ok(await _mediatr.Send(command));
    }
}
