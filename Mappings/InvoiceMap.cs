using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class InvoiceMap : EntityTypeConfiguration<Invoice>
    {
        public InvoiceMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable(nameof(Invoice));

            this.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("InvoiceId");

            this.Property(x => x.ProductOrderId)
                .HasColumnOrder(1);

            this.Property(x => x.UserId)
                .HasColumnOrder(2);

            this.Property(x => x.Status)
                .HasColumnOrder(3);

            this.Property(x => x.SubTotal)
                .HasColumnOrder(4);

            this.Property(x => x.PointsTotal)
                .HasColumnOrder(5);

            this.Property(x => x.TaxTotal)
                .HasColumnOrder(6);

            this.Property(x => x.Total)
                .HasColumnOrder(7);

            this.Property(x => x.Outstanding)
                .HasColumnOrder(8);

            this.Property(x => x.OutstandngPoints)
                .HasColumnOrder(9);

            this.HasRequired(x => x.ProductOrder)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.ProductOrderId);

            this.HasRequired(x => x.User)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);       
        }
    }
}
