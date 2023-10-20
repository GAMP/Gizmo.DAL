using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class RefundDepositPaymentMap : EntityTypeConfiguration<RefundDepositPayment>
    {
        public RefundDepositPaymentMap()
        {
            Property(t => t.DepositPaymentId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_DepositPayment") { IsUnique = true }
                }));

            Property(x => x.FiscalReceiptStatus)
                .HasColumnOrder(2);

            Property(x => x.FiscalReceiptId)
                .HasColumnOrder(3);

            HasOptional(t => t.DepositPayment)
                .WithMany()
                .HasForeignKey(x => x.DepositPaymentId)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.FiscalReceipt)
                .WithMany()
                .HasForeignKey(x => x.FiscalReceiptId)
                .WillCascadeOnDelete(false);

            ToTable(nameof(RefundDepositPayment));
        }
    }
}
