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
        {
            await _mediatr.Send(command);
            return View("CreateRoleView");
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> DeleteRole([FromForm] DeleteRoleCommand command)
        {
            await _mediatr.Send(command);
            return View("DeleteRoleView");
        }


        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdateRole([FromForm] UpdateRoleCommand command)
        {
            await _mediatr.Send(command);
            return View("UpdateRoleView");
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAllRole()
        {
            await _mediatr.Send(new GetAllRolesQuery());
            return View("GetAllRolesView");
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetByIdRole([FromQuery] GetByIdRoleQuery command)
        {
            await _mediatr.Send(command);
            return View("GetByIdRoleView");
        }
    }
}
