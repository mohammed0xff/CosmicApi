using CosmicApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CosmicApi.Infrastructure.Configuration
{
    public class StarConfiguration : IEntityTypeConfiguration<Star>
    {
        public void Configure(EntityTypeBuilder<Star> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(80);

            // planets
            builder.HasMany(x => x.Planets)
                    .WithOne(s => s.Star)
                    .HasForeignKey(x => x.StarId);

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
