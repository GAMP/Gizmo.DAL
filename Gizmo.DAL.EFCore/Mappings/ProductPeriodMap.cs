﻿using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLib;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class ProductPeriodMap : IEntityTypeConfiguration<ProductPeriod>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductPeriod> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Indexes
            builder.HasIndex(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            // Table & Column Mappings
            builder.ToTable("ProductPeriod");

            builder.Property(x => x.Id)
                .HasColumnName("ProductId");

            builder.HasOne(x => x.Product)
                .WithOne(x => x.Period)
                .HasForeignKey<ProductPeriod>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Seeds
            builder.HasData(new ProductPeriod() { Id = 7, Options = PeriodOptionType.None });
        }
    }
}
