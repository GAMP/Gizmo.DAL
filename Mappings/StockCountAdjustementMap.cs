using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// <see cref="StockCountAdjustement"/> mapping.
    /// </summary>
    public sealed class StockCountAdjustementMap : IEntityTypeConfiguration<StockCountAdjustement>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<StockCountAdjustement> builder)
        {
            builder.ToTable(nameof(StockCountAdjustement));

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
                .WithOne()
                .HasForeignKey<StockCountAdjustement>(x => x.StockCountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(stockCountAdjustment => stockCountAdjustment.InventoryAdjustment)
                .WithOne()
                .HasForeignKey<StockCountAdjustement>(stockCountAdjustment => stockCountAdjustment.AdjustmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
