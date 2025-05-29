using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Stock count entry map.
    /// </summary>
    public sealed class StockCountEntryMap : IEntityTypeConfiguration<StockCountEntry>
    {
        /// <inheritdoc/>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<StockCountEntry> builder)
        {
            builder.ToTable(nameof(StockCountEntry));

            builder.HasKey(stockCountEntry => stockCountEntry.Id);

            builder.Property(stockCountEntry => stockCountEntry.Id)
                .HasColumnOrder(0)
                .HasColumnName("StockCountEntryId")
                .IsRequired();

            builder.Property(stockCountEntry => stockCountEntry.StockCountId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(stockCountEntry => stockCountEntry.Expected)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(stockCountEntry => stockCountEntry.Actual)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(stockCountEntry => stockCountEntry.Difference)
                .HasColumnOrder(4)
                .IsRequired();

            builder.Property(stockCountEntry => stockCountEntry.Note)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(5)
                .IsRequired(false);

            builder.HasIndex(stockCountEntry => new { stockCountEntry.ProductId, stockCountEntry.StockCountId })
                .IsUnique()
                .HasFilter(null);

            builder.HasOne(stockCountEntry => stockCountEntry.StockCount)
                .WithMany(stockCount => stockCount.Entries)
                .HasForeignKey(stockCountEntry => stockCountEntry.StockCountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
