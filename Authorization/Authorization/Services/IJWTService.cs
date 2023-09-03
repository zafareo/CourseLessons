using Authorization.Models;

namespace Authorization.Services
{
    public interface IJWTService
    {
        public bool Authen(UserCredentials user);
        public string CreateToken(UserCredentials user);
    }
}
