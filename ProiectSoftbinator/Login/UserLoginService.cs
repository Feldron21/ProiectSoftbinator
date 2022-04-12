using ProiectSoftbinator.EN;
using ProiectSoftbinator.EN.Entities;
using ProiectSoftbinator.Login;
using ProiectSoftbinator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProiectSoftbinator.EN.Models.User;
using ProiectSoftbinator.Services.UserServices;

namespace ProiectSoftbinator.Login
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public UserLoginService(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }


        public async Task<UserLoginResponse> Authorize(UserLoginRequest user)
        {
            var Users = await _userService.GetAll();
            var existingUser = Users.FirstOrDefault(x => x.Email == user.Email);

            if (existingUser == null)
            {
                return new UserLoginResponse { Success = false };
            }
            else
            {
                if (existingUser.Password == user.Password)
                {
                    var refreshToken = _tokenService.CreateRefreshToken();
                    var accessToken = await _tokenService.GenerateToken(existingUser);
                    existingUser.RefreshToken = refreshToken;

                    return new UserLoginResponse
                    {
                        Success = true,
                        Token = accessToken,
                        RefreshToken = refreshToken
                    };
                }
                return new UserLoginResponse { Success = false };
            }
        }

        public async Task<UserLoginResponse> Reauthorize(TokenRefreshRequest tokenRefreshRequest)
        {
            var payload = _tokenService.GetPrincipalFromExpiredToken(tokenRefreshRequest.ExpiredToken);

            var email = payload.Identity?.Name;

            var Users = await _userService.GetAll();
            var user = Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return new UserLoginResponse { Success = false };
            }

            if (user.RefreshToken != tokenRefreshRequest.RefreshToken)
            {
                return new UserLoginResponse { Success = false };
            }

            return new UserLoginResponse
            {
                Success = true,
                Token = await _tokenService.GenerateToken(user),
                RefreshToken = tokenRefreshRequest.RefreshToken
            };
        }
    }
}