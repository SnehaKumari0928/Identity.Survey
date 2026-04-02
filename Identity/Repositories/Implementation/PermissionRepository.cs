using Identity.Data;
using Identity.Entities;
using Identity.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repositories.Implementation
{
    public class PermissionRepository: GenericRepository<Permission>,IPermissionRepository
    {
        public PermissionRepository(AppDbContext context): base(context) { }

        public async Task<Permission> GetByNameAsync(string name)
        {
            return await _context.Permissions.FindAsync(name);
        }
        public async Task<List<string>> GetRolesByPermissionIdAsync(int permissionId)
        {
            return await _context.RolePermissions
                .Where(rp => rp.PermissionId == permissionId)
                .Select(rp => rp.Role.Name).ToListAsync();
        }

        public async Task<List<string>> GetUserByPermissionIdAsync(int permissionId)
        {
            var usersFromRoles = await _context.RolePermissions
                .Where(rp => rp.PermissionId == permissionId)
                .SelectMany(rp => rp.Role.UserRoles)
                .Select(ur => ur.User.Email)
                .ToListAsync();

            var directUser =  await _context.UserPermissions
                .Where(rp => rp.PermissionId == permissionId)
                .Select(rp => rp.User.Email)
                .ToListAsync();

            return usersFromRoles.Union(directUser).Distinct().ToList();
        }

        public async Task<bool> IsPermissionAssignedToRole(int roleId,int permissionId)
        {
            return await _context.RolePermissions
                .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
        }
        public async Task<bool> IsPermissionAssignedToUser(int userId,int permissionId)
        {
            var hasRolePermission = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .SelectMany(ur => ur.Role.UserRoles)
                .AnyAsync(rp => rp.UserId == userId);

            var directPermission = await _context.UserPermissions
                .AnyAsync(up => up.UserId == userId && up.PermissionId == permissionId);

            return hasRolePermission || directPermission;
                
        }
    }
}
