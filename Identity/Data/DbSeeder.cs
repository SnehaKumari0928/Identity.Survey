using Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data
{
    public class DbSeeder
    {
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.MigrateAsync();

            var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");

            if(adminRole == null)
            {
                adminRole = new Entities.Role
                {
                    Name = "Admin",
                    Description = "Super Admin"
                };

                context.Roles.Add(adminRole);
                await context.SaveChangesAsync();
            }

            var adminUser = await context.Users
                .FirstOrDefaultAsync(u => u.Email == "mehak@gmail.com");

            if(adminUser == null)
            {
                adminUser = new User
                {
                    Email = "mehak@gmail.com",
                    HashedPassword = "sneha@12345",

                };

                context.Users.Add(adminUser);
                await context.SaveChangesAsync();


                var profile = new UserProfile
                {
                    UserId = adminUser.UserId,
                    Name = "Mehak",
                    Phone = "7689543278"
                };

                context.Profiles.Add(profile);
                await context.SaveChangesAsync();
                var userRole = new UserRole
                {
                    UserId = adminUser.UserId,
                    RoleId = adminRole.RoleId
                };

                context.UserRoles.Add(userRole);
                await context.SaveChangesAsync();


            }
        }
    }
}
