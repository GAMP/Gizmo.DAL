using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class InvoiceLineExtendedMap : IEntityTypeConfiguration<InvoiceLineExtended>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<InvoiceLineExtended> builder)
        {
            builder.ToTable(nameof(InvoiceLineExtended));

            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.BundleLineId)
                .HasColumnOrder(1);

            builder.Property(x => x.StockTransactionId)
                .HasColumnOrder(2);

            builder.Property(x => x.StockReturnTransactionId)
                .HasColumnOrder(3);

            // Indexes
            builder.HasIndex(x => x.StockTransactionId).IsUnique();
            builder.HasIndex(x => x.StockReturnTransactionId).IsUnique();
            builder.HasIndex(x => x.Id);

            builder.HasOne(x => x.BundleLine)
                .WithMany()
                .HasForeignKey(x => x.BundleLineId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.StockTransaction)
                .WithMany()
                .HasForeignKey(x => x.StockTransactionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
