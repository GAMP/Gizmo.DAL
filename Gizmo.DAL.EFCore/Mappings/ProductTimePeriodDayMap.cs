using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class ProductTimePeriodDayMap : IEntityTypeConfiguration<ProductTimePeriodDay>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductTimePeriodDay> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Indexes
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("ProductTimePeriodDay");

            builder.Property(x => x.Id)
                .HasColumnName("ProductTimePeriodDayId");

            builder.Property(x => x.ProductTimePeriodId)
                .HasColumnName("ProductTimePeriodId");
                
            // Indexes
            builder.HasIndex(x => new { x.ProductTimePeriodId, x.Day }, "UQ_ProductTimePeriodDay").IsUnique();

            builder.HasOne(x => x.Period)
                .WithMany(x => x.Days)
                .HasForeignKey(x => x.ProductTimePeriodId);
        }
    }
}
