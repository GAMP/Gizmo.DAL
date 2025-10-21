using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Deposit payment intent map.
    /// </summary>
    public class PaymentIntentDepositMap : IEntityTypeConfiguration<PaymentIntentDeposit>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PaymentIntentDeposit> builder)
        {
            builder.ToTable(nameof(PaymentIntentDeposit))
                .HasBaseType<PaymentIntent>();

            builder.Property(paymentIntentDeposit => paymentIntentDeposit.DepositPaymentId)
                .HasColumnOrder(1)
                .IsRequired(false);

            // Indexes
            builder.HasIndex(paymentIntentDeposit => paymentIntentDeposit.DepositPaymentId)
                .IsUnique();
            
            builder.HasIndex(paymentIntentDeposit => paymentIntentDeposit.Id);

            builder.HasOne(paymentIntentDeposit => paymentIntentDeposit.DepositPayment)
                .WithMany()
                .HasForeignKey(paymentIntentDeposit => paymentIntentDeposit.DepositPaymentId);    
        }
    }
}
