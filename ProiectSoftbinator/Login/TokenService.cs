using ProiectSoftbinator.EN;
using ProiectSoftbinator.EN.Entities;
using ProiectSoftbinator.EN.Models.User;
using ProiectSoftbinator.Services.RoleServices;
using ProiectSoftbinator.Services.UserServices;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ProiectSoftbinator.EN.Models.Role;

namespace ProiectSoftbinator.Login
{
    public class TokenService : ITokenService
    {
        private readonly IRoleService _roleService;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;


        public TokenService(IConfiguration _configuration, IRoleService roleService, IUserService userService)
        {
            this._configuration = _configuration;
            this._roleService = roleService;
            this._userService = userService;
        }

        public async Task<string> GenerateToken(UserGetModel user)
        {
            var userId = user.Id.ToString();
            var email = user.Email;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, email)
            };


            var roles = _userService.GetUserRole(Convert.ToInt32(user.Id));

            foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role, role));

            var secret = _configuration.GetSection("Jwt").GetSection("Token").Get<string>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = creds,
                Issuer = _configuration.GetSection("Jwt").GetSection("Issuer").Get<string>()
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Token"])),
                ValidateLifetime = true //here we are saying that we don't care about the token's expiration date
            };
            IdentityModelEventSource.ShowPII = true;
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            return principal;
        }
    }
}
