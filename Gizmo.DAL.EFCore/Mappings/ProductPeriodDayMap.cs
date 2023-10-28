using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Gizmo.DAL.Mappings
{
    public class ProductPeriodDayMap : IEntityTypeConfiguration<ProductPeriodDay>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductPeriodDay> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Table & Column Mappings
            builder.ToTable("ProductPeriodDay");

            builder.Property(x => x.Id)
                .HasColumnName("ProductPeriodDayId");

            builder.Property(x => x.ProductPeriodId)
                .HasColumnName("ProductPeriodId");

            // Indexes
            builder.HasIndex(x => new { x.ProductPeriodId, x.Day }).HasDatabaseName("UQ_ProductPeriodDay").IsUnique();

            builder.HasOne(x => x.Period)
                .WithMany(x => x.Days)
                .HasForeignKey(x => x.ProductPeriodId);

            // Seeds
            builder.HasData(new ProductPeriodDay() { Id = 7, ProductPeriodId = 7, Day = DayOfWeek.Saturday });
            builder.HasData(new ProductPeriodDay() { Id = 8, ProductPeriodId = 7, Day = DayOfWeek.Sunday });
        }
    }
}
