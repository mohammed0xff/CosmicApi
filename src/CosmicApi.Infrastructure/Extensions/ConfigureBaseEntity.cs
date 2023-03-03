using CosmicApi.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CosmicApi.Infrastructure.Extensions
{
    public static class EntityBuilderExtensions
    {
        public static void ConfigureBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : BaseEntity
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .IsRequired()
                .ValueGeneratedNever();
        }
    }
}
