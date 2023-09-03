using Authorization.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorization.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;
        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Authen(UserCredentials user)
        {
             return UserModel.Users.Exists
                (x=>x.Email.Equals(user.Email)
                &&
                x.Password.Equals(user.Password));
        }

        public string CreateToken(UserCredentials user)
        {
            var userA = UserModel.Users.FirstOrDefault
                (x => x.Email.Equals(user.Email)
                &&
                x.Password.Equals(user.Password));
            var permissions = new List<Claim>();
            foreach (var permission in userA.Permissions)
            {
                permissions.Add(new Claim(ClaimTypes.Role, permission));
            }
            Claim[] claims = new[]
            {
                new Claim("Email", user.Email),
                new Claim("Password", user.Password)
            };
            permissions.AddRange(claims);

            JwtSecurityToken token = new(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: permissions,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
                    SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
