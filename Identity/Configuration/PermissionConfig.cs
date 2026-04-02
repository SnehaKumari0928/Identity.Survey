using Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configuration
{
    public class PermissionConfig: IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> entity)
        {

            entity.HasKey(p => p.PermissionId);

            entity.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(50);

            entity.HasIndex(p => p.Name)
                .IsUnique();

            entity.Property(p => p.Description)
                .HasMaxLength(200);
        }
    }
}
