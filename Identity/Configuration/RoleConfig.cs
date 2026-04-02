using Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configuration
{
    public class RoleConfig: IEntityTypeConfiguration<Role>
    {

        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.HasKey(r =>  r.RoleId);

            entity.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(r => r.Name)
                .IsUnique();

            entity.Property(r => r.Description)
                .HasMaxLength(200);
        }
    }
}
