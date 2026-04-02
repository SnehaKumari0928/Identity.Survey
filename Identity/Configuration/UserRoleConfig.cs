using Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configuration
{
    public class UserRoleConfig: IEntityTypeConfiguration<UserRole>
    {

        public void Configure(EntityTypeBuilder<UserRole> entity)
        {
            entity.HasKey(ur => ur.Id);

            entity.HasIndex(ur => new
            {ur.UserId, ur.RoleId })
                .IsUnique();

            entity.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            entity.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);




                
        }
    }
}
