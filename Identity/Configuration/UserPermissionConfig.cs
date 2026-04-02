using Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Identity.Configuration
{
    public class UserPermissionConfig: IEntityTypeConfiguration<UserPermission>
    {

        public void Configure(EntityTypeBuilder<UserPermission> entity)
        {
            entity.HasKey(up => up.Id);

            entity.HasIndex(up => new { up.UserId, up.PermissionId })
                .IsUnique();

            entity.HasOne(up => up.User)
                .WithMany(u => u.UserPermissions)
                .HasForeignKey(up => up.UserId);

            entity.HasOne(up => up.Permission)
                .WithMany(p => p.UserPermissions)
                .HasForeignKey(up =>up.PermissionId);
                


        }
    }
}
