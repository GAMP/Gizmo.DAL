using GizmoDALV2.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class DepositPaymentMap : EntityTypeConfiguration<DepositPayment>
    {
        public DepositPaymentMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("DepositPaymentId");

            ToTable("DepositPayment");

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
        }
    }
}
