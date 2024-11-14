using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Inventory inbound mapping.
    /// </summary>
    public sealed class InventoryInboundMap : IEntityTypeConfiguration<InventoryInbound>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<InventoryInbound> builder)
        {
            builder.ToTable(nameof(InventoryInbound));
        }
    }
}
