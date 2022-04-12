using ProiectSoftbinator.EN.Models.User;
using System.Security.Claims;

namespace ProiectSoftbinator.Login
{
    public interface ITokenService
    {
        string CreateRefreshToken();
        Task<string> GenerateToken(UserGetModel user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}