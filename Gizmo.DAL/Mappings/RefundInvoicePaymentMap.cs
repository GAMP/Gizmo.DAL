using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class RefundInvoicePaymentMap : IEntityTypeConfiguration<RefundInvoicePayment>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<RefundInvoicePayment> builder)
        {
            builder.Property(t => t.InvoicePaymentId)
                .HasColumnOrder(1);

            builder.Property(t => t.InvoiceId)
                .HasColumnOrder(2);

            builder.HasOne(t => t.Invoice)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.InvoicePayment)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(t => t.InvoicePaymentId).IsUnique().HasFilter(null);
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable(nameof(RefundInvoicePayment));
        }
    }
}
