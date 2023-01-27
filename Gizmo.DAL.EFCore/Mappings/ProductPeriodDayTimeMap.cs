﻿using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class ProductPeriodDayTimeMap : IEntityTypeConfiguration<ProductPeriodDayTime>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductPeriodDayTime> builder)
        {
            // Key
            builder.HasKey(x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });

            // Table & Column Mappings
            builder.ToTable("ProductPeriodDayTime");

            // Indexes
            builder.HasIndex(t => t.PeriodDayId);

            builder.HasOne(x => x.Day)
                .WithMany(x => x.Times)
                .HasForeignKey(x => x.PeriodDayId);
        }
    }
}