using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class StockTransactionMap : EntityTypeConfiguration<StockTransaction>
    {
        public StockTransactionMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            ToTable(nameof(StockTransaction));

            Property(x => x.Id)
                .HasColumnName("StockTransactionId");

            // Relationships        
            HasRequired(x => x.Product)
                .WithMany(x => x.StockTransactions)
                .HasForeignKey(x => x.ProductId);

            HasOptional(x => x.SourceProduct)
                .WithMany(x=>x.StockTransactionsSource)
                .HasForeignKey(x => x.SourceProductId);

            HasOptional(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById);

            HasOptional(x => x.ModifiedBy)
                .WithMany()
                .HasForeignKey(x => x.ModifiedById);
        }
    }
}
