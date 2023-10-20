using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Deposit payment intent map.
    /// </summary>
    public class PaymentIntentDepositMap : EntityTypeConfiguration<PaymentIntentDeposit>
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        public PaymentIntentDepositMap()
        {
            Property(x => x.DepositPaymentId)
                .HasColumnOrder(1)
                .IsOptional()
                .HasColumnAnnotation("Index", new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_DepositPayment") { IsUnique = true } //same deposit payment may not appear multiple times
                }));

            HasOptional(x => x.DepositPayment)
                .WithMany()
                .HasForeignKey(x => x.DepositPaymentId);

            ToTable(nameof(PaymentIntentDeposit));
        }
    }
}
