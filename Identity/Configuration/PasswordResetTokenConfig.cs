using Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Identity.Configuration
{
    public class PasswordResetTokenConfig: IEntityTypeConfiguration<PasswordResetToken>
    {
        public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
        {
            builder.HasOne(ps => ps.User)
                .WithMany(u => u.PasswordResetTokens)
                .HasForeignKey(ps => ps.UserId);


        }

    }
}
