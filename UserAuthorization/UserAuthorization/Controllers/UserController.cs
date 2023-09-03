using Application.Interfaces;
using Application.Services;
using Domain.Models;
using Domain.Models.Token;
using Domain.Models.UserCredential;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;

namespace UserAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        
        private readonly IUserRepository _repository;
        private readonly IJWTservice _jwtService;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly IConfiguration _configuration;
        public UserController(IUserRepository repository, IJWTservice jwtService, IUserRefreshTokenRepository userRefreshTokenRepository, IConfiguration configuration)
        {
            _repository = repository;
            _jwtService = jwtService;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        
        public async Task<IActionResult> RefreshToken(Token token)
        {
            Log.Information($"{nameof(RefreshToken)}");
            var principal = _jwtService.GetPrincipalFromExpiredToken(token.AccessToken);
            var name = principal.FindFirstValue(ClaimTypes.Name);
            var user = await _repository.GetAsync(x => x.UserName == name);
            var credential = new UserCredential
            {
                UserName = user.UserName,
                Password = user.Password
            };

            UserRefreshToken savedRefreshToken = await _userRefreshTokenRepository.GetSavedRefreshTokens(name, token.RefreshToken);
            if (savedRefreshToken == null || savedRefreshToken.RefreshToken != token.RefreshToken)
            {
                return Unauthorized("Invalid input");
            }
            if(savedRefreshToken.Expiretime < DateTime.UtcNow)
            {
                return Unauthorized(" time limit of the token has expired !");
            }

            var newJwt = await _jwtService.GenerateTokenAsync(user);
            if (newJwt == null)
            {
                return Unauthorized("Invalid input");
            }
            int min = 4;
            if (int.TryParse(_configuration["JWT:RefreshTokenExpiresTime"], out int _min))
            {
                min = _min;
            }
            UserRefreshToken refreshToken = new()
            {
                RefreshToken = newJwt.RefreshToken,
                UserName = name,
                Expiretime = DateTime.UtcNow.AddMinutes(min)
            };
            bool IsDeleted = await _userRefreshTokenRepository.DeleteUserRefreshTokens(name, token.RefreshToken);
            if (IsDeleted)
            {
                await _userRefreshTokenRepository.AddUserRefreshTokens(refreshToken);
            }
            else
            {
                return BadRequest();
            }
            return Ok(newJwt);
            
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserCredential  credential)
        {
            Log.Information("Login is called");
            string hashedPsw = await _repository.ComputeHashAsync(credential.Password);
            User? user = await _repository.GetAsync(x => x.UserName == credential.UserName && x.Password == hashedPsw);
            if (!await _userRefreshTokenRepository.IsValidUserAsync(user))
            {
                return Unauthorized();
            }
            int min = 4;
            if (int.TryParse(_configuration["JWT:RefreshTokenExpiresTime"], out int _min))
            {
                min = _min;
            }
            var token = await _jwtService.GenerateTokenAsync(user);
            var refreshToken = new UserRefreshToken
            {
                UserName = user.UserName,
                Expiretime = DateTime.UtcNow.AddMinutes(min),
                RefreshToken = token.RefreshToken
            };
            await _userRefreshTokenRepository.UpdateUserRefreshToken(refreshToken);
            return Ok(token);
            
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            Log.Information($"Created {user.UserName}");
            if (ModelState.IsValid)
            {
                bool IsSuccess = await _repository.CreateAsync(user);
                if (IsSuccess)
                {
                    return Ok(user);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]{id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            Log.Information($"User Id: {Id}");
            User? user = await _repository.GetAsync(x=>x.UsersId == Id);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            Log.Information($"{nameof(GetAll)}");
            var res = (await _repository.GetAllAsync()).Include(x => x.UserRoles).Select(x => new
            {
                x.UsersId,
                x.UserName,
                Role = x.UserRoles.Select(t => new
                {
                    t.Role.Id,
                    t.Role.Name
                })
            });
            return Ok(res);

        }


        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            Log.Information($" Updated : {user.UserName}");
            if (ModelState.IsValid)
            {
                bool IsSuccess = await _repository.UpdateAsync(user);
                if (IsSuccess)
                {
                    return Ok(user);
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("[action]{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Log.Information($"Deleted {id}");
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
