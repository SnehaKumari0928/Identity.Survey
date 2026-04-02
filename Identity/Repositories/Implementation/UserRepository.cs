using Identity.Data;
using Identity.Entities;
using Identity.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repositories.Implementation
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context): base(context) { }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FindAsync(email);
        }
        public async Task<User> GetUserWithRolesAsync(int userId)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }
        public async Task<List<string>> GetUserRolesAsync(int userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.Name)
                .ToListAsync();


        }

        public async Task<List<string>> GetUserPermissionsAsync(int userId)
        {

            var rolePermissions = await _context.UserRoles
                                .Where(ur => ur.UserId == userId)
                                .SelectMany(ur => ur.Role.RolePermissions)
                                .Select(rp => rp.Permission.Name)
                                .ToListAsync();

           
            var userPermissions = await _context.UserPermissions
                .Where(up => up.UserId == userId)
                .Select(up => up.Permission.Name)
                .ToListAsync();

            return rolePermissions
                .Union(userPermissions)
                .Distinct()
                .ToList();
        }
    }
}
