using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Order payment intent map.
    /// </summary>
    public class PaymentIntentOrderMap : IEntityTypeConfiguration<PaymentIntentOrder>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<PaymentIntentOrder> builder)
        {
            builder.ToTable(nameof(PaymentIntentOrder));

            builder.Property(paymentIntentOrder => paymentIntentOrder.AutoComplete)
                .IsRequired(true)
                .HasColumnOrder(0);

            builder.Property(paymentIntentOrder => paymentIntentOrder.DisableReceiptPrinting)
                .IsRequired(true)
                .HasColumnOrder(1);

            builder.HasMany(paymentIntentOrder => paymentIntentOrder.Orders)
                .WithOne(paymentIntentOrderOrder => paymentIntentOrderOrder.PaymentIntentOrder)
                .HasForeignKey(paymentIntentOrderOrder => paymentIntentOrderOrder.PaymentIntentOrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(paymentIntentOrder => paymentIntentOrder.Deposits)
                .WithOne(paymentIntentOrderDeposit => paymentIntentOrderDeposit.PaymentIntentOrder)
                .HasForeignKey(paymentIntentOrderDeposit => paymentIntentOrderDeposit.PaymentIntentOrderId)
                .OnDelete(DeleteBehavior.NoAction);        
        }
    }
}
