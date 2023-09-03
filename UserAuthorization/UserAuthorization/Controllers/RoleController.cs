using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace UserAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _repository;
        public RoleController(IRoleRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] Role role)
        {
            Log.Information($"{nameof(Create)} {role.Name}");
            if (ModelState.IsValid)
            {
                bool IsSuccess = await _repository.CreateAsync(role);
                if (IsSuccess)
                {
                    return Ok(role);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Log.Information($"{nameof(GetById)}");
            Role? role = await _repository.GetAsync(x=>x.Id == id);
            if (role != null)
            {
                return Ok(role);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            Log.Information($"{GetAll}");
            var roles = await _repository.GetAllAsync();
            if (roles != null)
            {
                return Ok(roles);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] Role role)
        {
            Log.Information($"{nameof(GetById)}");
            if (ModelState.IsValid)
            {
                bool IsSuccess = await _repository.UpdateAsync(role);
                if (IsSuccess)
                {
                    return Ok(role);
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("[action]{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Log.Information($"{nameof(Delete)}{id}");
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
