using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Inventory adjustment mapping.
    /// </summary>
    public sealed class InventoryAdjustmentMap : IEntityTypeConfiguration<InventoryAdjustment>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<InventoryAdjustment> builder)
        {
            builder.ToTable(nameof(InventoryAdjustment))
                .HasBaseType<Inventory>();

            builder.Property(adjustment => adjustment.AdjustmentType)
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(adjustment => adjustment.InvoiceId)
                .HasColumnOrder(1)
                .IsRequired(false);
        }
    }
}
