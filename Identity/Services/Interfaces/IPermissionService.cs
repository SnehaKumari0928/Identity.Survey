using Identity.DTOs.Permission;

namespace Identity.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<List<PermissionDto>> GetAllPermissionsAsync();
        Task<PermissionDto> GetPermissionByIdAsync(int permissionId);
        Task<PermissionDto> CreatePermissionAsync(CreatePermissionDto dto);

        Task DeletePermissionAsync(int permissionId);

        Task<List<string>> GetRolesByPermissionAsync(int permissionId);


        Task<List<string>> GetUsersByPermissionAsync(int permissionId);

        Task<bool> IsPermissionAssignedToRoleAsync(int roleId, int permissionId);
        Task<bool> IsPermissionAssignedToUserAsync(int userId, int permissionId);
    }
}
