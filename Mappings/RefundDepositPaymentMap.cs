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

            HasRequired(t => t.DepositPayment)
                .WithMany()
                .WillCascadeOnDelete(false);

            ToTable(nameof(RefundDepositPaymentMap));
        }
    }
}
