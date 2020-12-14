using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class DepositPaymentMap : EntityTypeConfiguration<DepositPayment>
    {
        public DepositPaymentMap()
        {
            this.HasKey(x => x.Id);

            this.Property(x => x.Id)
                .HasColumnName("DepositPaymentId");

            this.ToTable("DepositPayment");

            this.HasRequired(x => x.Payment)
                .WithMany()
                .HasForeignKey(x => x.PaymentId);

            this.HasRequired(x => x.DepositTransaction)
                .WithMany()
                .HasForeignKey(x => x.DepositTransactionId);

            this.HasRequired(x => x.User)
                .WithMany(x => x.DepositPayments)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
