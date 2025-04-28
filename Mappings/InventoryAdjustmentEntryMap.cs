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
            
            builder.Property(adjustmentEntry => adjustmentEntry.Id)
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(adjustmentEntry => adjustmentEntry.UnitCost)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(adjustmentEntry => adjustmentEntry.TotalCost)
                .IsRequired()
                .HasColumnOrder(2);
            
            builder.Property(adjustmentEntry => adjustmentEntry.UnitPrice)
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(adjustmentEntry => adjustmentEntry.TotalPrice)
                .IsRequired()
                .HasColumnOrder(4);

            builder.Property(adjustmentEntry => adjustmentEntry.TotalPrice)
                .IsRequired()
                .HasColumnOrder(5);
        }
    }
}
