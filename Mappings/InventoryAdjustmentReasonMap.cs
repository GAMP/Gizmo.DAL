using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Inventory adjustment reason entity map.
    /// </summary>
    public sealed class InventoryAdjustmentReasonMap : IEntityTypeConfiguration<InventoryAdjustmentReason>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<InventoryAdjustmentReason> builder)
        {
            builder.ToTable(nameof(InventoryAdjustmentReason));

            builder.HasKey(inventoryAdjustmentReason => inventoryAdjustmentReason.Id);

            builder.Property(inventoryAdjustmentReason => inventoryAdjustmentReason.Id)
                .HasColumnName("InventoryAdjustmentReasonId")
                .HasColumnOrder(0);

            builder.Property(inventoryAdjustmentReason => inventoryAdjustmentReason.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(inventoryAdjustmentReason => inventoryAdjustmentReason.Description)
                .HasColumnOrder(2)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(inventoryAdjustmentReason => inventoryAdjustmentReason.IsDeleted)
                .HasColumnOrder(3)
                .IsRequired();

            builder.HasMany(inventoryAdjustmentReason => inventoryAdjustmentReason.AdjustmentEntries)
                .WithOne(inventoryAdjustmentEntry => inventoryAdjustmentEntry.AdjustmentReason)
                .HasForeignKey(inventoryAdjustmentEntry => inventoryAdjustmentEntry.AdjustmentReasonId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
