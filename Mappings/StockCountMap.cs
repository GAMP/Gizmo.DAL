using Gizmo.DAL.Entities;
using Gizmo.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Stock count map.
    /// </summary>
    public sealed class StockCountMap : IEntityTypeConfiguration<StockCount>
    {
        /// <summary>
        /// Creates new instance of <see cref="StockCountMap"/>.
        /// </summary>
        public StockCountMap() 
        {
        }

        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<StockCount> builder)
        {
            builder.ToTable(nameof(StockCount));
            builder.HasKey(stockCount => stockCount.Id);

            builder.Property(stockCount => stockCount.Id)
                .HasColumnName("StockCountId")
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(stockCount => stockCount.StockId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(stockCount => stockCount.Type)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(stockCount => stockCount.UnexpectedEntries)
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(stockCount => stockCount.Note)
                .IsRequired(false)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            builder.HasOne(stockCount => stockCount.Stock)
                .WithMany(stock => stock.Counts)
                .HasForeignKey(stockCount => stockCount.StockId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
