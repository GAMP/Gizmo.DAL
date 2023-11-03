﻿using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class ProductTimeMap : IEntityTypeConfiguration<ProductTime>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductTime> builder)
        {
            // Table & Column Mappings
            builder.ToTable("ProductTime");

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.AppGroup)
                .WithMany(x => x.TimeOffers)
                .HasForeignKey(x => x.AppGroupId);
        }
    }
}
