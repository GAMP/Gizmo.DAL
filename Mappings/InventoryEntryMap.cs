using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Inventory entry entity map.
    /// </summary>
    public sealed class InventoryEntryMap : IEntityTypeConfiguration<InventoryEntry>
    {
        /// <inheritdoc/>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<InventoryEntry> builder)
        {
            builder.ToTable(nameof(InventoryEntry))
                .UseTptMappingStrategy();

            builder.HasKey(inventoryEntry => inventoryEntry.Id);

            builder.Property(inventoryEntry => inventoryEntry.Id)
                .HasColumnName("InventoryEntryId")
                .HasColumnOrder(0);

            builder.Property(inventoryEntry => inventoryEntry.InventoryId)
                .HasColumnOrder(1);

            builder.Property(inventoryEntry => inventoryEntry.StockId)
                .HasColumnOrder(2);

            builder.Property(inventoryEntry => inventoryEntry.ProductId) 
                .HasColumnOrder(3);

            builder.Property(inventoryEntry => inventoryEntry.StockTransactionId)
                .HasColumnOrder(4);

            builder.Property(inventoryEntry => inventoryEntry.Quantity)
                .HasColumnOrder(5);

            builder.Property(inventoryEntry => inventoryEntry.Note)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(6);

            builder.Property(inventoryEntry => inventoryEntry.ShiftId)
                .HasColumnOrder(7);

            builder.HasOne(inventoryEntry => inventoryEntry.Inventory)
                .WithMany(inventory => inventory.Entries)
                .HasForeignKey(inventoryEntry => inventoryEntry.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(inventoryEntry => inventoryEntry.StockTransaction)
               .WithMany(stockTransaction => stockTransaction.InventoryEntries)
               .HasForeignKey(inventoryEntry => inventoryEntry.StockTransactionId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(inventoryEntry => inventoryEntry.Stock)
                .WithMany(stock => stock.InventoryEntries)
                .HasForeignKey(inventoryEntry => inventoryEntry.StockId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(inventoryEntry => inventoryEntry.Product)
                .WithMany(product => product.InventoryEntries)
                .HasForeignKey(inventoryEntry => inventoryEntry.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
