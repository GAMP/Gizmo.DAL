using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class StockTransactionMap : IEntityTypeConfiguration<StockTransaction>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<StockTransaction> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.ToTable(nameof(StockTransaction));

            builder.Property(x => x.Id)
                .HasColumnName("StockTransactionId");

            // Relationships        
            builder.HasOne(x => x.Product)
                .WithMany(x => x.StockTransactions)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.SourceProduct)
                .WithMany(x=>x.StockTransactionsSource)
                .HasForeignKey(x => x.SourceProductId);

            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.ModifiedBy)
                .WithMany()
                .HasForeignKey(x => x.ModifiedById);
        }
    }
}
