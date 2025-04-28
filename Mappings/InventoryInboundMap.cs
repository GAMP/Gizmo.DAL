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
            builder.ToTable(nameof(InventoryInbound))
                .HasBaseType<Inventory>();

            builder.Property(inventoryInbound => inventoryInbound.Id)
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(inventoryInbound => inventoryInbound.Cost)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(inventoryInbound => inventoryInbound.InventoryTransferId)
                .HasColumnOrder(2)
                .IsRequired(false);

            builder.HasOne(inbound => inbound.InventoryTransfer)
               .WithOne()
               .HasForeignKey<InventoryInbound>(inbound => inbound.InventoryTransferId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
