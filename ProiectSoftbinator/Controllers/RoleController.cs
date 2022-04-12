using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProiectSoftbinator.EN.Models.Role;
using ProiectSoftbinator.Services.RoleServices;

namespace ProiectSoftbinator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("getAllRoles")]
        public async Task<ActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAll();

            return Ok(roles);
        }

        [HttpPost("add-role")]
        public async Task<ActionResult> AddRole([FromBody] RolePostModel model)
        {
            await _roleService.AddRole(model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            await _roleService.DeleteRole(id);

            return Ok();
        }
    }
}

