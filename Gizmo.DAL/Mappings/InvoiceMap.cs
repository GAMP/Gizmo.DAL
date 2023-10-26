using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class InvoiceMap : EntityTypeConfiguration<Invoice>
    {
        public InvoiceMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("InvoiceId");

            Property(x => x.ProductOrderId)
                .HasColumnOrder(1);

            Property(x => x.UserId)
                .HasColumnOrder(2);

            Property(x => x.Status)
                .HasColumnOrder(3);

            Property(x => x.SubTotal)
                .HasColumnOrder(4);

            Property(x => x.PointsTotal)
                .HasColumnOrder(5);

            Property(x => x.TaxTotal)
                .HasColumnOrder(6);

            Property(x => x.Total)
                .HasColumnOrder(7);

            Property(x => x.Outstanding)
                .HasColumnOrder(8);

            Property(x => x.OutstandngPoints)
                .HasColumnOrder(9);

            Property(x => x.ShiftId)
                .HasColumnOrder(10);

            Property(x => x.RegisterId)
                .HasColumnOrder(11);

            Property(x => x.IsVoided)
                .HasColumnOrder(12);

            Property(x => x.SaleFiscalReceiptStatus)
                .HasColumnOrder(13);

            Property(x => x.ReturnFiscalReceiptStatus)
                .HasColumnOrder(14);

            HasRequired(x => x.ProductOrder)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.ProductOrderId);

            HasRequired(x => x.User)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            ToTable(nameof(Invoice));
        }
    }
}
