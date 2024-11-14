using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Inventory adjustment entry map.
    /// </summary>
    public sealed class InventoryAdjustmentEntryMap : IEntityTypeConfiguration<InventoryAdjustmentEntry>
    {
        /// <inheritdoc/>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<InventoryAdjustmentEntry> builder)
        {
            builder.ToTable(nameof(InventoryAdjustmentEntry))
                .HasBaseType<InventoryEntry>();

            builder.Property(InventoryAdjustmentEntry => InventoryAdjustmentEntry.UnitCost)
                .IsRequired()
                .HasColumnOrder(0);

            builder.Property(InventoryAdjustmentEntry => InventoryAdjustmentEntry.TotalCost)
                .IsRequired()
                .HasColumnOrder(1);
            
            builder.Property(InventoryAdjustmentEntry => InventoryAdjustmentEntry.UnitPrice)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(InventoryAdjustmentEntry => InventoryAdjustmentEntry.TotalPrice)
                .IsRequired()
                .HasColumnOrder(3);
        }
    }
}
