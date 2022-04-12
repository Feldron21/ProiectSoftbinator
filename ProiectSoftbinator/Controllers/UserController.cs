using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProiectSoftbinator.EN.Models.User;
using ProiectSoftbinator.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using ProiectSoftbinator.EN.Entities;
using ProiectSoftbinator.Services.RoleServices;
using ProiectSoftbinator.EN.Models.Role;

namespace ProiectSoftbinator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet("getAllUsers")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _userService.GetAll();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var users = await _userService.GetById(id);

            return Ok(users);
        }

        [HttpPost("add-user")]
        public async Task<ActionResult> AddUser([FromBody] UserPostModel model)
        {
            var allAvailableRoles = await _roleService.GetAll();

            List<RoleGetModel> userRoles = new List<RoleGetModel>();

            foreach(var role in model.Roles)
            {
                if(allAvailableRoles.Where(u=>u.Name == role).FirstOrDefault() == null)
                {
                    throw new ArgumentException($"Specified role {role} does not exist!");
                }
                else
                {
                    userRoles.Add(allAvailableRoles.Where(u => u.Name == role).FirstOrDefault());
                }
            }

            await _userService.AddUser(model,userRoles);

            return Ok();
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateUser([FromBody] UserPutModel model, int id)
        {
            await _userService.UpdateUser(model, id);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);

            return Ok();
        }

        [HttpGet()]

        public async Task<ActionResult> GetUserRole(int id)
        {
            await _userService.GetUserRole(id);

            return Ok();
        }
    }
}
