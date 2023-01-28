using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class InvoiceMap : IEntityTypeConfiguration<Invoice>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("InvoiceId");

            builder.Property(x => x.ProductOrderId)
                 .HasColumnOrder(1);

            builder.Property(x => x.UserId)
                 .HasColumnOrder(2);

            builder.Property(x => x.Status)
                 .HasColumnOrder(3);

            builder.Property(x => x.SubTotal)
                 .HasColumnOrder(4);

            builder.Property(x => x.PointsTotal)
                 .HasColumnOrder(5);

            builder.Property(x => x.TaxTotal)
                 .HasColumnOrder(6);

            builder.Property(x => x.Total)
                 .HasColumnOrder(7);

            builder.Property(x => x.Outstanding)
                 .HasColumnOrder(8);

            builder.Property(x => x.OutstandngPoints)
                 .HasColumnOrder(9);

            builder.Property(x => x.ShiftId)
                 .HasColumnOrder(10);

            builder.Property(x => x.RegisterId)
                 .HasColumnOrder(11);

            builder.Property(x => x.IsVoided)
                 .HasColumnOrder(12);

            builder.Property(x => x.SaleFiscalReceiptStatus)
                 .HasColumnOrder(13);

            builder.Property(x => x.ReturnFiscalReceiptStatus)
                 .HasColumnOrder(14);

            builder.HasOne(x => x.ProductOrder)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.ProductOrderId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(nameof(Invoice));
        }
    }
}
