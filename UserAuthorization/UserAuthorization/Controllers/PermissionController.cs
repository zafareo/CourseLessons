using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace UserAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {

        private readonly IPermissionRepository _repository;
        private readonly IPermissionRepository repository1;
        public PermissionController(IPermissionRepository repository)
        {
            _repository = repository;
            repository1 = repository;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] Permission permission)
        {
            Log.Information($"{nameof(Create)} {permission.PermissionName}");
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
            Permission? permission = await _repository.GetAsync(x => x.PermissionId == id);
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
            var permissions = await _repository.GetAllAsync();
            if (permissions != null)
            {
                return Ok(permissions);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] Permission permission)
        {
            Log.Information($"{nameof(Update)} {permission.PermissionId}");
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
            Log.Information($"{nameof(Delete)}{id} deleted");
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
