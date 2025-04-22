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
        }
    }
}
