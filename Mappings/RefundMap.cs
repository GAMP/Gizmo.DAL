using GizmoDALV2.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class RefundMap : EntityTypeConfiguration<Refund>
    {
        public RefundMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasColumnName("RefundId")
                .HasColumnOrder(0);

            this.Property(t => t.PaymentId)
                .HasColumnOrder(1);

            this.Property(t => t.Amount)
                .HasColumnOrder(2);

            this.Property(t => t.DepositTransactionId)
                .HasColumnOrder(3);

            this.Property(t => t.RefundMethodId)
                .HasColumnOrder(4);

            this.HasRequired(t => t.Payment)
                .WithMany()
                .HasForeignKey(t => t.PaymentId);

            this.HasRequired(t => t.RefundMethod)
                .WithMany()
                .HasForeignKey(t => t.RefundMethodId)
                .WillCascadeOnDelete(false);

            this.HasOptional(t => t.Shift)
                .WithMany(t => t.Refunds)
                .WillCascadeOnDelete(false);
           

            // Table & Column Mappings
            this.ToTable(nameof(Refund));
        }
    }

    public class RefundInvoicePaymentMap: EntityTypeConfiguration<RefundInvoicePayment>
    {
        public RefundInvoicePaymentMap()
        {
            this.Property(t => t.InvoicePaymentId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index",new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_InvoicePayment") { IsUnique = true }
                }));

            this.Property(t => t.InvoiceId)
                .HasColumnOrder(2);

            this.HasRequired(t => t.Invoice)
                .WithMany()
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.InvoicePayment)
                .WithMany()
                .WillCascadeOnDelete(false);

            // Table & Column Mappings
            this.ToTable(nameof(RefundInvoicePayment));
        }
    }
}
