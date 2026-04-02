using Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configuration
{
    public class RefreshTokenConfig: IEntityTypeConfiguration<RefreshToken>
    {

        public void Configure(EntityTypeBuilder<RefreshToken> entity)
        {

            entity.HasKey(rt => rt.Id);

            entity.Property(rt => rt.Token)
                .IsRequired();

            entity.HasIndex(rt => rt.Token)
                .IsUnique();

            entity.Property(rt => rt.ExpiryDate)
                .IsRequired();

            entity.Property(rt => rt.IsRevoked)
                .HasDefaultValue(false);

            entity.HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId);
        }
    }
}
