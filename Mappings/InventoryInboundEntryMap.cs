using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Inventory inbound entry entity map.
    /// </summary>
    public sealed class InventoryInboundEntryMap : IEntityTypeConfiguration<InventoryInboundEntry>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<InventoryInboundEntry> builder)
        {
            builder.ToTable(nameof(InventoryInboundEntry))
                .HasBaseType<InventoryEntry>();

            builder.Property(inboundEntry => inboundEntry.Id)
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(inboundEntry => inboundEntry.UnitCost)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(inboundEntry => inboundEntry.TotalCost)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(inboundEntry => inboundEntry.ExpirationDate)
                .HasColumnOrder(4)
                .IsRequired(false);

            builder.Property(inboundEntry => inboundEntry.InventoryTransferEntryId)
                .HasColumnOrder(3)
                .IsRequired(false);

            builder.HasOne(inboundEntry => inboundEntry.InventoryTransferEntry)
                .WithOne()
                .HasForeignKey<InventoryInboundEntry>(inboundEntry => inboundEntry.InventoryTransferEntryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
