using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class InvoiceLineProductMap : IEntityTypeConfiguration<InvoiceLineProduct>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<InvoiceLineProduct> builder)
        {
            builder.ToTable(nameof(InvoiceLineProduct));

            // Indexes
            builder.HasIndex(t => t.OrderLineId).HasDatabaseName("UQ_OrderLine").IsUnique();
            builder.HasIndex(t => t.Id);
            
            builder.HasOne(x => x.Product)
                .WithMany(x => x.InvoiceLines)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.OrderLine)
                .WithMany()
                .HasForeignKey(x => x.OrderLineId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
