using CosmicApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CosmicApi.Infrastructure.Extensions;

namespace CosmicApi.Infrastructure.Configuration
{
    public class GalaxyConfiguration : IEntityTypeConfiguration<Galaxy>
    {
        public void Configure(EntityTypeBuilder<Galaxy> builder)
        {
            builder.ConfigureBaseEntity();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(80);

            // stars
            builder.HasMany(x => x.Stars)
                .WithOne(s => s.Galaxy)
                .HasForeignKey(x => x.GalaxyId);
            // planets
            builder.HasMany(x => x.Planets)
                .WithOne(p => p.Galaxy)
                .HasForeignKey(x => x.GalaxyId);
            // blackholes
            builder.HasMany(x => x.BlackHoles)
                .WithOne(b => b.Galaxy)
                .HasForeignKey(x => x.GalaxyId);


            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
