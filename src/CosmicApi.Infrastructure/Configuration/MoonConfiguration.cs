using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CosmicApi.Infrastructure.Configuration
{
    public class MoonConfiguration : IEntityTypeConfiguration<Moon>
    {
        public void Configure(EntityTypeBuilder<Moon> builder)
        {
            builder.ConfigureBaseEntity();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(80);

            builder.HasOne(x => x.Planet)
                .WithMany(p => p.Moons)
                .HasForeignKey(x => x.PlanetId);

        }
    }
}
