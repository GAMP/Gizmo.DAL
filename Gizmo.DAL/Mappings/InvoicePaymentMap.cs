using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class InvoicePaymentMap : EntityTypeConfiguration<InvoicePayment>
    {
        public InvoicePaymentMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("InvoicePaymentId");

            Property(x => x.InvoiceId)
                .HasColumnOrder(1);

            Property(x => x.PaymentId)
                .HasColumnOrder(2);

            Property(x => x.UserId)
                .HasColumnOrder(3);

            ToTable(nameof(InvoicePayment));

            HasRequired(x => x.Invoice)
                .WithMany(x => x.InvoicePayments)
                .HasForeignKey(x => x.InvoiceId);

            HasRequired(x => x.Payment)
                .WithMany()
                .HasForeignKey(x => x.PaymentId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.User)
                .WithMany(x => x.InvoicePayments)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
