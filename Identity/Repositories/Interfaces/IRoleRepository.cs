using Identity.Entities;

namespace Identity.Repositories.Interfaces
{
    public interface IRoleRepository: IGenericRepository<Role>
    {
        Task<Role> GetRoleWithPermissionsAsync(int roleId);
        Task<List<string>> GetPemissionsByRoleId(int roleId);

      

        Task AssignPermissionsAsync(int roleId, List<int> permissionIds);
    }
}
