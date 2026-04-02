using Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configuration
{
    public class UserConfig: IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(u => u.UserId);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(u => u.Email)
                .IsUnique();

            entity.Property(u => u.HashedPassword)
                .IsRequired();

            entity.Property(u => u.IsActive)
                .HasDefaultValue(true);
            entity.Property(u => u.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
