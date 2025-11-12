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
            builder.ToTable(nameof(PaymentIntentOrder))
                .HasBaseType<PaymentIntent>();

            builder.Property(paymentIntentOrder => paymentIntentOrder.AutoComplete)
                .IsRequired(true)
                .HasColumnOrder(0);

            builder.HasMany(paymentIntentOrder => paymentIntentOrder.IntentOrders)
                .WithOne(paymentIntentOrderOrder => paymentIntentOrderOrder.PaymentIntentOrder)
                .HasForeignKey(paymentIntentOrderOrder => paymentIntentOrderOrder.PaymentIntentOrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(paymentIntentOrder => paymentIntentOrder.IntentDeposits)
                .WithOne(paymentIntentOrderDeposit => paymentIntentOrderDeposit.PaymentIntentOrder)
                .HasForeignKey(paymentIntentOrderDeposit => paymentIntentOrderDeposit.PaymentIntentOrderId)
                .OnDelete(DeleteBehavior.NoAction);        
        }
    }
}
