using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        }
    }
}
