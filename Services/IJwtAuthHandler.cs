using Prueba.Model;

namespace Prueba.Services
{
    public interface IJwtAuthHandler 
    {
        JwtUserModel AuthenticateUser(JwtUserModel userInfo);
        string GenerateJwtTokens(JwtUserModel userInfo);
    }
}
