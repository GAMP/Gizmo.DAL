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
        }
    }
}
