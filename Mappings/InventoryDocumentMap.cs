using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Inventory document map.
    /// </summary>
    public sealed class InventoryDocumentMap : IEntityTypeConfiguration<InventoryDocument>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<InventoryDocument> builder)
        {
            builder.ToTable(nameof(InventoryDocument));

            builder.HasKey(inventoryDocument => inventoryDocument.Id);

            builder.Property(inventoryDocument => inventoryDocument.Id)
                .HasColumnName("InventoryDocumentId")
                .HasColumnOrder(0)
                .IsRequired();

            builder.HasIndex(inventoryDocument => new { inventoryDocument.InventoryId, inventoryDocument.DocumentId })
                .IsUnique()
                .HasFilter(null);

            builder.HasOne(inventoryDocument => inventoryDocument.Inventory)
                .WithMany(inventory => inventory.Documents)
                .HasForeignKey(inventoryDocument => inventoryDocument.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(inventoryDocument => inventoryDocument.Document)
                .WithMany(inventory => inventory.InventoryDocuments)
                .HasForeignKey(inventoryDocument => inventoryDocument.DocumentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
