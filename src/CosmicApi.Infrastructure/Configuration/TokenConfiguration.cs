using CosmicApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using CosmicApi.Infrastructure.Extensions;

namespace CosmicApi.Infrastructure.Configuration
{
    public class TokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ConfigureBaseEntity();

            builder.HasOne(x => x.User)
                .WithOne(x => x.RefreshToken);
            
        }
    }
}
