using Application.MediatrEntities.Permissions.Commands;
using Application.MediatrEntities.Permissions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace GapKo_p.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ApiBaseController
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePermission([FromForm] CreatePermissionCommand command)
      => Ok(await _mediatr.Send(command));

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> DeletePermission([FromForm] DeletePermissionCommand command)
            => Ok(await _mediatr.Send(command));


        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdatePermission([FromForm] UpdatePermissionCommand command)
            => Ok(await _mediatr.Send(command));


        [HttpGet("getall")]
        public async Task<IActionResult> GetAllPermission()
            => Ok(await _mediatr.Send(new GetAllPermissionsQuery()));

        [HttpGet("getById")]
        public async Task<IActionResult> GetByIdPermission([FromQuery] GetByIdPermissionQuery command)
            => Ok(await _mediatr.Send(command));
    }
}
