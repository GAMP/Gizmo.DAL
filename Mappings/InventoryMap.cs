using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Inventory entity map.
    /// </summary>
    public sealed class InventoryMap : IEntityTypeConfiguration<Inventory>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable(nameof(Inventory))
                .UseTptMappingStrategy();

            builder.HasKey(inventory => inventory.Id);

            builder.Property(inventory => inventory.Id)
                .HasColumnName("InventoryId")
                .HasColumnOrder(0)
                .IsRequired();
        }
    }
}
