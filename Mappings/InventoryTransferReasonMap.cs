using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Inventory adjustment reason entity map.
    /// </summary>
    public sealed class InventoryTransferReasonMap : IEntityTypeConfiguration<InventoryTransferReason>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<InventoryTransferReason> builder)
        {
            builder.ToTable(nameof(InventoryTransferReason));

            builder.HasKey(inventoryAdjustmentReason => inventoryAdjustmentReason.Id);

            builder.Property(inventoryTransferReason => inventoryTransferReason.Id)
                .ValueGeneratedNever()
                .HasColumnName("InventoryTransferReasonId")
                .HasColumnOrder(0);

            builder.Property(inventoryTransferReason => inventoryTransferReason.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(inventoryTransferReason => inventoryTransferReason.Description)
                .HasColumnOrder(2)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(inventoryTransferReason => inventoryTransferReason.IsDeleted)
                .HasColumnOrder(3)
                .IsRequired();

            builder.HasIndex(inventoryTransferReason => inventoryTransferReason.Name).IsUnique();

            builder.HasMany(inventoryTransferReason => inventoryTransferReason.TransferEntries)
                .WithOne(inventoryTransferEntry => inventoryTransferEntry.TransferReason)
                .HasForeignKey(inventoryTransferEntry => inventoryTransferEntry.TransferReasonId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
