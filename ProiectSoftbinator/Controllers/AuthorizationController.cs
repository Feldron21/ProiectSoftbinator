using ProiectSoftbinator.Models;
using ProiectSoftbinator.Login;
using Microsoft.AspNetCore.Mvc;

namespace ProiectSoftbinator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserLoginService userLoginService;

        public AuthorizationController(IUserLoginService userLoginService)
        {
            this.userLoginService = userLoginService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginRequest user)
        {
            var authenticationInfo = await userLoginService.Authorize(user);

            if (authenticationInfo.Success)
            {
                return Ok(authenticationInfo);
            }
            else
            {
                return BadRequest("Verify Login data");
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenRefreshRequest tokenRefreshRequest)
        {
            var reauthenticationInfo = await userLoginService.Reauthorize(tokenRefreshRequest);

            if (reauthenticationInfo.Success)
            {
                return Ok(reauthenticationInfo);
            }
            else
            {
                return BadRequest("Failed to reauthorize");
            }
        }
    }
}
