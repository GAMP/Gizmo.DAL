using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Inventory transfer mapping.
    /// </summary>
    public sealed class InventoryTransferMap : IEntityTypeConfiguration<InventoryTransfer>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<InventoryTransfer> builder)
        {
            builder.ToTable(nameof(InventoryTransfer))
                .HasBaseType<Inventory>();

            builder.Property(transfer => transfer.Id)
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(transfer => transfer.TransferStockId)
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(transfer => transfer.InventoryInboundId)
                .HasColumnOrder(1)
                .IsRequired(false);

            builder.HasOne(transfer => transfer.TransferStock)
                .WithMany()
                .HasForeignKey(transfer => transfer.TransferStockId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(transfer => transfer.InventoryInbound)
                .WithMany()
                .HasForeignKey(transfer => transfer.InventoryInboundId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
