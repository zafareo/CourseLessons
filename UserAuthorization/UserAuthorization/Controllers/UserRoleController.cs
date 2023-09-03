using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace UserAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleRepository _repository;
        public UserRoleController(IUserRoleRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] UserRole userRole)
        {
            Log.Information($"created {userRole}");
            if (ModelState.IsValid)
            {
                bool IsSuccess = await _repository.CreateAsync(userRole);
                if (IsSuccess)
                {
                    return Ok(userRole);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Log.Information($"UserRole Id {id}");
            UserRole? userRole = await _repository.GetAsync(x=>x.UserRoleId == id);
            if (userRole != null)
            {
                return Ok(userRole);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            Log.Information($"{nameof(GetAll)}");
            var userRoles = await _repository.GetAllAsync();
            if (userRoles != null)
            {
                return Ok(userRoles);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] UserRole userRole)
        {
            Log.Information($"{nameof(Update)}");
            if (ModelState.IsValid)
            {
                bool IsSuccess = await _repository.UpdateAsync(userRole);
                if (IsSuccess)
                {
                    return Ok(userRole);
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("[action]{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Log.Information($"{nameof(Delete)}");
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
