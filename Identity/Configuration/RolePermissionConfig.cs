using Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configuration
{
    public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
    {

        public void Configure(EntityTypeBuilder<RolePermission> entity)
        {
            entity.HasKey(rp => rp.Id);

            entity.HasIndex(rp => new
            {
                rp.RoleId,
                rp.PermissionId
            }).IsUnique();

            entity.HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            entity.HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            
        }
    }
}
