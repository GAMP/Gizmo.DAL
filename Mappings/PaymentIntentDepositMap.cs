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
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<PaymentIntentDeposit> builder)
        {
            builder.Property(x => x.DepositPaymentId)
                .HasColumnOrder(1)
                .IsRequired(false);

            // Indexes
            builder.HasIndex(t => t.DepositPaymentId).IsUnique();
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.DepositPayment)
                .WithMany()
                .HasForeignKey(x => x.DepositPaymentId);

            builder.ToTable(nameof(PaymentIntentDeposit));
        }
    }
}
