using GizmoDALV2.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class RefundInvoicePaymentMap : EntityTypeConfiguration<RefundInvoicePayment>
    {
        public RefundInvoicePaymentMap()
        {
            Property(t => t.InvoicePaymentId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_InvoicePayment") { IsUnique = true }
                }));

            Property(t => t.InvoiceId)
                .HasColumnOrder(2);

            HasRequired(t => t.Invoice)
                .WithMany()
                .WillCascadeOnDelete(false);

            HasRequired(t => t.InvoicePayment)
                .WithMany()
                .WillCascadeOnDelete(false);

            // Table & Column Mappings
            ToTable(nameof(RefundInvoicePayment));
        }
    }
}
