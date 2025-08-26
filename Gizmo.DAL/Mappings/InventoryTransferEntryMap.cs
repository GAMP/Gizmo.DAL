using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Inventory transfer entry map.
    /// </summary>
    public sealed class InventoryTransferEntryMap : IEntityTypeConfiguration<InventoryTransferEntry>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<InventoryTransferEntry> builder)
        {
            builder.ToTable(nameof(InventoryTransferEntry))
                .HasBaseType<InventoryEntry>();

            builder.Property(transferEntry => transferEntry.Id)
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(transferEntry => transferEntry.TransferReasonId)
                .HasColumnOrder(1)
                .IsRequired(false);
        }
    }
}
