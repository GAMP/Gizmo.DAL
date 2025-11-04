using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Invoice payment refund entity.
    /// </summary>
    public class RefundInvoicePaymentMap : IEntityTypeConfiguration<RefundInvoicePayment>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<RefundInvoicePayment> builder)
        {
            builder.Property(refundInvoicePayment => refundInvoicePayment.InvoicePaymentId)
                .HasColumnOrder(1);

            builder.Property(refundInvoicePayment => refundInvoicePayment.InvoiceId)
                .HasColumnOrder(2);

            builder.HasOne(refundInvoicePayment => refundInvoicePayment.Invoice)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(refundInvoicePayment => refundInvoicePayment.InvoicePaymentId).IsUnique().HasFilter(null);
            builder.HasIndex(refundInvoicePayment => refundInvoicePayment.Id);

            builder.HasOne(refundInvoicePayment => refundInvoicePayment.InvoicePayment)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(refundInvoicePayment => refundInvoicePayment.Invoice)
               .WithMany(invoice => invoice.InvoicePaymentRefunds)
               .HasForeignKey(refundInvoicePayment => refundInvoicePayment.InvoiceId)
               .OnDelete(DeleteBehavior.Restrict);

            // Table & Column Mappings
            builder.ToTable(nameof(RefundInvoicePayment));
        }
    }
}
