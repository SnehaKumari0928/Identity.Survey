using Identity.Entities;

namespace Identity.Repositories.Interfaces
{
    public interface IPermissionRepository
    {
        Task<Permission> GetByNameAsync(string name);
        Task<List<string>> GetRolesByPermissionIdAsync(int  permissionId);

        Task<List<string>> GetUserByPermissionIdAsync(int permissionId);

        Task<bool> IsPermissionAssignedToRole(int roleId,int permissionId);
        Task<bool> IsPermissionAssignedToUser(int userId,int permissionId);

    }
}
