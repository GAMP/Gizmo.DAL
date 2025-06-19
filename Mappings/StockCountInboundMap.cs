using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// <see cref="StockCountInbound"/> mapping.
    /// </summary>
    public sealed class StockCountInboundMap : IEntityTypeConfiguration<StockCountInbound>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<StockCountInbound> builder)
        {
            builder.ToTable(nameof(StockCountInbound));

            builder.HasKey(stockCountInbound => stockCountInbound.StockCountId);
            builder.Property(stockCountInbound => stockCountInbound.StockCountId)
                .HasColumnName("StockCountId")
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(stockCountInbound => stockCountInbound.InboundId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.HasIndex(stockCountInbound => stockCountInbound.InboundId)
                .IsUnique()
                .HasFilter(null);

            builder.HasOne(x => x.StockCount)
                .WithOne(x => x.Inbound)                
                .HasForeignKey<StockCountInbound>(x => x.StockCountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(stockCountInbound => stockCountInbound.InventoryInbound)
                .WithOne()
                .HasForeignKey<StockCountInbound>(stockCountInbound => stockCountInbound.InboundId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
