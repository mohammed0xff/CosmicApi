using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CosmicApi.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x => x.Email).IsRequired().HasMaxLength(254);
        builder.Property(x => x.Username).IsRequired().HasMaxLength(60);

        builder.HasOne<RefreshToken>()
            .WithOne(x => x.User)
            .HasForeignKey<RefreshToken>(x => x.UserId);
        
        builder.HasIndex(x => x.Email).IsUnique();
    }
}