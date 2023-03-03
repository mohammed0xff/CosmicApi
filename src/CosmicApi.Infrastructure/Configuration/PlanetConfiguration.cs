using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CosmicApi.Infrastructure.Configuration
{
    public class PlanetConfiguration : IEntityTypeConfiguration<Planet>
    {
        public void Configure(EntityTypeBuilder<Planet> builder)
        {
            builder.ConfigureBaseEntity();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(80);

            // Moons
            builder.HasMany(x => x.Moons)
                    .WithOne(s => s.Planet)
                    .HasForeignKey(x => x.PlanetId)
                    .OnDelete(DeleteBehavior.NoAction);
            // Star
            builder.HasOne(x => x.Star)
                    .WithMany(s => s.Planets)
                    .HasForeignKey(x => x.StarId)
                    .OnDelete(DeleteBehavior.NoAction);
            // Galaxy
            builder.HasOne(x => x.Galaxy)
                .WithMany(s => s.Planets)
                .HasForeignKey(x => x.GalaxyId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
