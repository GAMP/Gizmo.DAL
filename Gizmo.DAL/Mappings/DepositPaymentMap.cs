using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class DepositPaymentMap : EntityTypeConfiguration<DepositPayment>
    {
        public DepositPaymentMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("DepositPaymentId");

            Property(x => x.DepositTransactionId)
                .HasColumnOrder(1);

            Property(x => x.PaymentId)
                .HasColumnOrder(2);

            Property(x => x.ShiftId)
                .HasColumnOrder(3);

            Property(x => x.RegisterId)
                .HasColumnOrder(4);

            Property(x => x.RefundedAmount)
                .HasColumnOrder(5);

            Property(x => x.RefundStatus)
                .HasColumnOrder(6);

            Property(x => x.FiscalReceiptStatus)
              .HasColumnOrder(7);

            Property(x => x.FiscalReceiptId)
                .HasColumnOrder(8);

            Property(x => x.IsVoided)
                .HasColumnOrder(9);

            HasRequired(x => x.Payment)
                .WithMany()
                .HasForeignKey(x => x.PaymentId);

            HasRequired(x => x.DepositTransaction)
                .WithMany()
                .HasForeignKey(x => x.DepositTransactionId);

            HasRequired(x => x.User)
                .WithMany(x => x.DepositPayments)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            ToTable(nameof(DepositPayment));
        }
    }
}
