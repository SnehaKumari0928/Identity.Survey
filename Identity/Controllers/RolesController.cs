using Identity.DTOs.Role;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {

        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            return Ok(role);
        }

        [HttpGet("{id}/permissions")]
        public async Task<IActionResult> GetRolePermissions(int id)
        {
            var permissions = await _roleService.GetRolePemissionsAsync(id);
            return Ok(permissions);
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetRolesWithPermission(int id)
        {
            var roles = await _roleService.GetRoleWithPermissionsAsync(id);
            return Ok(roles);
        }

        [HttpPost("createrole")]
        [Authorize("admin")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto dto)
        {
            var created = await _roleService.CreateRoleAsync(dto);
            return CreatedAtAction(nameof(GetRoleById), new { id = created.RoleId }, created);
        }

        [HttpPut("{id}/updaterole")]
        [Authorize("admin")]
        public async Task<IActionResult> UpdateRole(int id,[FromBody] UpdateRoleDto dto)
        {
            var created = await _roleService.UpdateRoleAsync(id,dto);
            return CreatedAtAction(nameof(GetRoleById), new { id = created.RoleId }, created);
        }

        [HttpDelete("{id}/deleterole")]
        [Authorize("admin")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _roleService.DeleteRoleAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/permissions")]
        [Authorize("admin")]
        public async Task<IActionResult> AssignPermissions([FromBody] AssignPermissionToRoleDto dto)
        {
            await _roleService.AssignPermissionsAsync(dto);
            return NoContent();
        }
        
    }
}
