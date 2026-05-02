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


        public async Task AssignPermissionsAsync(int roleId, List<int> permissionIds)
        {
            var existingPermissionIds = await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.PermissionId)
                .ToListAsync();

            var newPermissionIds = permissionIds
                .Where(pid => !existingPermissionIds.Contains(pid))
                .ToList();

            var newMappings = newPermissionIds.Select(pid => new RolePermission
            {
                RoleId = roleId,
                PermissionId = pid
            }).ToList();

            await _context.RolePermissions.AddRangeAsync(newMappings);

            await _context.SaveChangesAsync();
        }
    }
}
