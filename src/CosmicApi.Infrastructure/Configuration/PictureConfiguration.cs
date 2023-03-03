﻿using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CosmicApi.Infrastructure.Configuration
{
    public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.ConfigureBaseEntity();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(80);
        }
    }
}
