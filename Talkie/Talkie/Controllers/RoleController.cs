using Application.MediatrEntities.Roles.Commands;
using Application.MediatrEntities.Roles.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GapKo_p.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ApiBaseController
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRole([FromForm] CreateRoleCommand command)
         => Ok(await _mediatr.Send(command));

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> DeleteRole([FromForm] DeleteRoleCommand command)
            => Ok(await _mediatr.Send(command));


        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdateRole([FromForm] UpdateRoleCommand command)
            => Ok(await _mediatr.Send(command));


        [HttpGet("getall")]
        public async Task<IActionResult> GetAllRole()
            => Ok(await _mediatr.Send(new GetAllRolesQuery()));

        [HttpGet("getById")]
        public async Task<IActionResult> GetByIdRole([FromQuery] GetByIdRoleQuery command)
            => Ok(await _mediatr.Send(command));
    }
}
