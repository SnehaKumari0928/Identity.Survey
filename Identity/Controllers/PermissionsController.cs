using Identity.DTOs.Permission;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            var permissions = await _permissionService.GetAllPermissionsAsync();
            return Ok(permissions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermissionById(int id)
        {
            var permission = await _permissionService.GetPermissionByIdAsync(id);
            return Ok(permission);
        }

        [HttpPost]
        [Authorize("Admin")]
        public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionDto dto)
        {
           var created = await _permissionService.CreatePermissionAsync(dto);
            return CreatedAtAction(nameof(GetPermissionById), new { id = created.PermissionId }, created);

        }

        [HttpDelete]
        [Authorize("Admin")]
        public async Task<IActionResult> DeletePermissions(int id)
        {
            await _permissionService.DeletePermissionAsync(id);
            return Ok();
        }

        [HttpGet("{id}/roles")]
        [Authorize("Admin")]
        public async Task<IActionResult> GetRolesByPermission(int permissionId)
        {
            var result = await _permissionService.GetRolesByPermissionAsync(permissionId);
            return Ok(result);
        }

        [HttpGet("{id}/users")]
        [Authorize("Admin")]
        public async Task<IActionResult> GetUsersByPermission(int permissionId)
        {
            var result = await _permissionService.GetUsersByPermissionAsync(permissionId);
            return Ok(result);
        }
    }
}
