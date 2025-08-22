using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Payment intent order - order map.
    /// </summary>
    public sealed class IntentOrderMap : IEntityTypeConfiguration<IntentOrder>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<IntentOrder> builder)
        {
            builder.ToTable(nameof(IntentOrder));

            builder.HasKey(intentOrder => intentOrder.Id);
            builder.Property(paymentIntentOrder => paymentIntentOrder.Id)
                .HasColumnName("IntentOrderId")
                .HasColumnOrder(0);

            builder.Property(intentOrder => intentOrder.PaymentIntentOrderId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(intentOrder => intentOrder.ProductOrderId)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(intentOrder => intentOrder.InvoicePaymentId)
                .IsRequired(false)
                .HasColumnOrder(3);

            // same order should not appear multiple times for the same payment intent order
            builder.HasIndex(paymentIntentOrder => new { paymentIntentOrder.PaymentIntentOrderId, paymentIntentOrder.ProductOrderId }).IsUnique();
            
            //same invoice payment may not appear multiple times
            builder.HasIndex(intentOrder => intentOrder.InvoicePaymentId).IsUnique(); 

            builder.HasOne(intentOrder => intentOrder.PaymentIntentOrder)
                .WithMany(paymentIntentOrder => paymentIntentOrder.Orders)
                .HasForeignKey(intentOrder => intentOrder.PaymentIntentOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(intentOrder => intentOrder.ProductOrder)
                .WithMany()
                .HasForeignKey(paymentIntentOrder => paymentIntentOrder.ProductOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(intentOrder => intentOrder.InvoicePayment)
                .WithMany()
                .HasForeignKey(intentOrder => intentOrder.InvoicePaymentId);
        }
    }
}
