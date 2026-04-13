using Identity.Entities;
using Identity.DTOs;
using Identity.DTOs.Role;

namespace Identity.Services.Interfaces
{
    public interface IRoleService
    {

        Task<RoleResponseDto> GetRoleWithPermissionsAsync(int roleId);
        Task<List<string>> GetRolePemissionsAsync(int roleId);

        Task<RoleResponseDto> GetRoleByIdAsync(int roleId);

        Task<List<RoleResponseDto>> GetAllRolesAsync();
        Task<RoleResponseDto> CreateRoleAsync(CreateRoleDto dto);

        Task<RoleResponseDto> UpdateRoleAsync(int roleId,UpdateRoleDto dto);
        Task DeleteRoleAsync(int roleId);

        Task AssignPermissionsAsync(int roleId, List<int> permissionIds);
    }
}
