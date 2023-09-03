using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace UserAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionRepository _repository;
        public RolePermissionController(IRolePermissionRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] RolePermission permission)
        {
            Log.Information($"{nameof(Create)}");
            if (ModelState.IsValid)
            {
                bool IsSuccess = await _repository.CreateAsync(permission);
                if (IsSuccess)
                {
                    return Ok(permission);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Log.Information($"{nameof(GetById)} Id {id}");
            RolePermission? permission = await _repository.GetAsync(x=>x.PermissionId == id);
            if (permission != null)
            {
                return Ok(permission);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            Log.Information($"{nameof(GetAll)}");
            var rolePermissions = await _repository.GetAllAsync();
            if (rolePermissions != null)
            {
                return Ok(rolePermissions);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] RolePermission permission)
        {
            Log.Information($"{nameof(Update)}");
            if (ModelState.IsValid)
            {
                bool IsSuccess = await _repository.UpdateAsync(permission);
                if (IsSuccess)
                {
                    return Ok(permission);
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("[action]{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Log.Information($"{nameof(Delete)} {id} deleted");
            if (ModelState.IsValid)
            {
                bool IsSuccess = await _repository.DeleteAsync(id);
                if (IsSuccess)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
