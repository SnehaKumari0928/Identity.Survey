using Identity.Entities;

namespace Identity.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleWithPermissionsAsync(int roleId);
        Task<List<string>> GetPemissionsByRoleId(int roleId);
    }
}
