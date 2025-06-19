using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// <see cref="StockCountAdjustment"/> mapping.
    /// </summary>
    public sealed class StockCountAdjustmentMap : IEntityTypeConfiguration<StockCountAdjustment>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<StockCountAdjustment> builder)
        {
            builder.ToTable(nameof(StockCountAdjustment));

            builder.HasKey(stockCountAdjustment => stockCountAdjustment.StockCountId);
            builder.Property(stockCountAdjustment => stockCountAdjustment.StockCountId)
                .HasColumnName("StockCountId")
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(stockCountAdjustment => stockCountAdjustment.AdjustmentId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.HasIndex(stockCountAdjustment => stockCountAdjustment.AdjustmentId)
                .IsUnique()
                .HasFilter(null);

            builder.HasOne(x => x.StockCount)
                .WithOne(x => x.Adjustment)
                .HasForeignKey<StockCountAdjustment>(x => x.StockCountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(stockCountAdjustment => stockCountAdjustment.InventoryAdjustment)
                .WithOne()
                .HasForeignKey<StockCountAdjustment>(stockCountAdjustment => stockCountAdjustment.AdjustmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
