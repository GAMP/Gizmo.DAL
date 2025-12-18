using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Payment intent order - order map.
    /// </summary>
    public sealed class IntentOrderDepositMap : IEntityTypeConfiguration<IntentOrderDeposit>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<IntentOrderDeposit> builder)
        {
            builder.ToTable(nameof(IntentOrderDeposit));

            builder.HasKey(paymentIntentOrder => paymentIntentOrder.Id);
            builder.Property(paymentIntentOrder => paymentIntentOrder.Id)
                .HasColumnName("IntentOrderDepositId")
                .HasColumnOrder(0);

            builder.Property(paymentIntentOrder => paymentIntentOrder.PaymentIntentOrderId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(paymentIntentOrder => paymentIntentOrder.UserId)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(paymentIntentOrder => paymentIntentOrder.Amount)
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(paymentIntentOrder => paymentIntentOrder.DepositPaymentId)
                .IsRequired(false)
                .HasColumnOrder(4);

            // same user should not appear multiple times for the same payment intent order
            builder.HasIndex(paymentIntentOrder => new { paymentIntentOrder.PaymentIntentOrderId, paymentIntentOrder.UserId }).IsUnique();

            builder.HasOne(paymentIntentOrder => paymentIntentOrder.PaymentIntentOrder)
                .WithMany(paymentIntentOrder => paymentIntentOrder.IntentDeposits)
                .HasForeignKey(paymentIntentOrder => paymentIntentOrder.PaymentIntentOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(paymentIntentOrderDeposit => paymentIntentOrderDeposit.DepositPayment)
                .WithMany()
                .HasForeignKey(paymentIntentOrderDeposit => paymentIntentOrderDeposit.DepositPaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(paymentIntentOrderDeposit => paymentIntentOrderDeposit.User)
                .WithMany()
                .HasForeignKey(paymentIntentOrderDeposit => paymentIntentOrderDeposit.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
