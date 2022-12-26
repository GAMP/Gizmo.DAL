using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class InvoicePaymentMap : EntityTypeConfiguration<InvoicePayment>
    {
        public InvoicePaymentMap()
        {
            this.HasKey(x => x.Id);

            this.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("InvoicePaymentId");

            this.Property(x => x.InvoiceId)
                .HasColumnOrder(1);

            this.Property(x => x.PaymentId)
                .HasColumnOrder(2);

            this.Property(x => x.UserId)
                .HasColumnOrder(3);

            this.ToTable(nameof(InvoicePayment));

            this.HasRequired(x => x.Invoice)
                .WithMany(x => x.InvoicePayments)
                .HasForeignKey(x => x.InvoiceId);

            this.HasRequired(x => x.Payment)
                .WithMany()
                .HasForeignKey(x => x.PaymentId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.User)
                .WithMany(x=>x.InvoicePayments)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
