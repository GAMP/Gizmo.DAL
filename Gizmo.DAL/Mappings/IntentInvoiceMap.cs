using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Intent invoice entity map.
    /// </summary>
    public sealed class IntentInvoiceMap : IEntityTypeConfiguration<IntentInvoice>
    {
        /// <inheritdoc/>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<IntentInvoice> builder)
        {
            builder.ToTable(nameof(IntentInvoice));

            builder.HasKey(intentInvoice => intentInvoice.Id);
            builder.Property(paymentIntentOrder => paymentIntentOrder.Id)
                .HasColumnName("IntentInvoiceId")
                .HasColumnOrder(0);

            builder.Property(intentInvoice => intentInvoice.PaymentIntentOrderId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(intentInvoice => intentInvoice.InvoiceId)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(intentInvoice => intentInvoice.Amount)
               .IsRequired()
               .HasColumnOrder(3);

            builder.Property(intentInvoice => intentInvoice.InvoicePaymentId)
                .IsRequired(false)
                .HasColumnOrder(4);

            // same invoice should not appear multiple times for the same payment intent order
            builder.HasIndex(intentInvoice => new { intentInvoice.PaymentIntentOrderId, intentInvoice.InvoiceId }).IsUnique();

            //same invoice payment may not appear multiple times
            builder.HasIndex(intentInvoice => intentInvoice.InvoicePaymentId).IsUnique();

            builder.HasOne(intentInvoice => intentInvoice.PaymentIntentOrder)
                .WithMany(paymentIntentOrder => paymentIntentOrder.Invoices)
                .HasForeignKey(intentInvoice => intentInvoice.PaymentIntentOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(intentInvoice => intentInvoice.Invoice)
                .WithMany()
                .HasForeignKey(intentInvoice => intentInvoice.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(intentInvoice => intentInvoice.InvoicePayment)
                .WithMany()
                .HasForeignKey(intentInvoice => intentInvoice.InvoicePaymentId);
        }
    }
}
