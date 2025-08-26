using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Deposit payment entity map.
    /// </summary>
    public class DepositPaymentMap : IEntityTypeConfiguration<DepositPayment>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DepositPayment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("DepositPaymentId");

            builder.Property(x => x.DepositTransactionId)
                .HasColumnOrder(1);

            builder.Property(x => x.PaymentId)
                .HasColumnOrder(2);

            builder.Property(x => x.Amount)
                .HasColumnOrder(3)
                .HasPrecision(19, 4);

            builder.Property(x => x.ShiftId)
                .HasColumnOrder(4);

            builder.Property(x => x.RegisterId)
                .HasColumnOrder(5);

            builder.Property(x => x.RefundedAmount)
                .HasColumnOrder(6);

            builder.Property(x => x.RefundStatus)
                .HasColumnOrder(7);

            builder.Property(x => x.FiscalReceiptStatus)
              .HasColumnOrder(8);

            builder.Property(x => x.FiscalReceiptId)
                .HasColumnOrder(9);

            builder.Property(x => x.IsVoided)
                .HasColumnOrder(10);

            builder.HasOne(x => x.Payment)
                .WithMany()
                .HasForeignKey(x => x.PaymentId);

            builder.HasOne(x => x.DepositTransaction)
                .WithMany()
                .HasForeignKey(x => x.DepositTransactionId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.DepositPayments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(nameof(DepositPayment));
        }
    }
}
