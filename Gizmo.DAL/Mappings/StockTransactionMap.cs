using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Stock transaction map.
    /// </summary>
    public class StockTransactionMap : IEntityTypeConfiguration<StockTransaction>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<StockTransaction> builder)
        {
            // Primary Key
            builder.HasKey(stockTransaction => stockTransaction.Id);

            // Properties
            builder.ToTable(nameof(StockTransaction));

            builder.Property(stockTransaction => stockTransaction.Id)
                .HasColumnName("StockTransactionId");

            // Relationships        
            builder.HasOne(stockTransaction => stockTransaction.Product)
                .WithMany(stockTransaction => stockTransaction.StockTransactions)
                .HasForeignKey(stockTransaction => stockTransaction.ProductId);

            builder.HasOne(stockTransaction => stockTransaction.SourceProduct)
                .WithMany(stockTransaction => stockTransaction.StockTransactionsSource)
                .HasForeignKey(stockTransaction => stockTransaction.SourceProductId);

            builder.HasOne(stockTransaction => stockTransaction.CreatedBy)
                .WithMany()
                .HasForeignKey(stockTransaction => stockTransaction.CreatedById);

            builder.HasOne(stockTransaction => stockTransaction.ModifiedBy)
                .WithMany()
                .HasForeignKey(stockTransaction => stockTransaction.ModifiedById);

            builder.HasOne(stockTransaction => stockTransaction.Stock)
                .WithMany(stock => stock.Transactions)
                .HasForeignKey(stockTransaction => stockTransaction.StockId)
                .OnDelete(DeleteBehavior.Restrict);

            // Stock reports (ProductStockReport / ProductTransactionReport in Service.Reports) filter the
            // ~400K-row table by a CreatedTime range (optionally + ProductId) and otherwise full-scan it
            // (~10,900 logical reads for a handful of in-period rows). CreatedTime leads because it is the
            // always-present filter; ProductId is the optional second predicate. (The covering composite for
            // the per-sale latest-OnHand read lives in ApplyPerformanceIndexes — it needs INCLUDE.)
            builder.HasIndex(stockTransaction => new { stockTransaction.CreatedTime, stockTransaction.ProductId });
        }
    }
}
