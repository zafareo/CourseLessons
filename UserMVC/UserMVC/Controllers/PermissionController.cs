using Application.MediatrEntities.Permissions.Commands;
using Application.MediatrEntities.Permissions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace GapKo_p.Controllers
{
 //   [Route("api/[controller]")]
    //[ApiController]
    public class PermissionController : ApiBaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePermission([FromForm] CreatePermissionCommand command)
        {
            await _mediatr.Send(command);
            return View("CreatePermission");
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> DeletePermission([FromForm] DeletePermissionCommand command)
        {
            await _mediatr.Send(command);
            return View("DeletePermission");
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdatePermission([FromForm] UpdatePermissionCommand command)
        {
            await _mediatr.Send(command);
            return View("UpdatePermission");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermission()
        {
            IQueryable<Application.Common.DTO.PermissionGetDTO> res = await _mediatr.Send(new GetAllPermissionsQuery());
            return View("GetAllPermissions",res.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdPermission([FromQuery] GetByIdPermissionQuery command)
        {
            await _mediatr.Send(command);
            return View("GetByIdPermission");
        }
    }
}
