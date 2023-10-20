using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Order payment intent map.
    /// </summary>
    public class PaymentIntentOrderMap : EntityTypeConfiguration<PaymentIntentOrder>
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        public PaymentIntentOrderMap()
        {
            Property(x => x.ProductOrderId)
                .IsOptional()
                .HasColumnOrder(1);

            Property(x => x.InvoicePaymentId)
                .IsOptional()
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_InvoicePayment") { IsUnique = true } //same invoice payment may not appear multiple times
                }));

            HasOptional(x => x.ProductOrder)
                .WithMany(x => x.PaymentIntents)
                .HasForeignKey(x => x.ProductOrderId);

            HasOptional(x => x.InvoicePayment)
                .WithMany()
                .HasForeignKey(x => x.InvoicePaymentId);

            ToTable(nameof(PaymentIntentOrder));
        }
    }
}
