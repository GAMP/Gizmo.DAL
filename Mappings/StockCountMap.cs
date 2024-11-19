using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Stock count map.
    /// </summary>
    public sealed class StockCountMap : IEntityTypeConfiguration<StockCount>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<StockCount> builder)
        {
            builder.ToTable(nameof(StockCount));
            builder.HasKey(stockCount => stockCount.Id);

            builder.Property(stockCount => stockCount.Id)
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(stockCount => stockCount.Note)
                .IsRequired(false)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(stockCount => stockCount.UnexpectedEntries)
                .IsRequired()
                .HasColumnOrder(2);

            builder.HasOne(stockCount => stockCount.Stock)
                .WithMany(stock => stock.Counts)
                .HasForeignKey(stockCount => stockCount.StockId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
