using ProiectSoftbinator.Models;

namespace ProiectSoftbinator.Login
{
    public interface IUserLoginService
    {
        Task<UserLoginResponse> Authorize(UserLoginRequest user);
        Task<UserLoginResponse> Reauthorize(TokenRefreshRequest tokenRefreshRequest);
    }
}