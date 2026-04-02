using Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configuration
{
    public class UserProfileConfig: IEntityTypeConfiguration<UserProfile>
    {

        public void Configure(EntityTypeBuilder<UserProfile> entity)
        {
            entity.HasKey(up => up.Id);

            entity.Property(up => up.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(up=> up.Phone)
                .IsRequired()
                .HasMaxLength(15);


            entity.HasOne(up => up.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<UserProfile>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
        }
    }
}
