using Identity.Data;
using Identity.Entities;
using Identity.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repositories.Implementation
{
    public class RoleRepository: GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context):base(context) { }

        public async Task<Role> GetRoleWithPermissionsAsync(int roleId)
        {
           return  await _context.Roles
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(r => r.RoleId == roleId);
        }
        public async Task<List<string>> GetPemissionsByRoleId(int roleId)
        {
            return await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.Permission.Name)
                .ToListAsync();
        }
    }
}
