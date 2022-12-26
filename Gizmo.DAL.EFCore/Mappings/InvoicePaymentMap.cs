using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class InvoicePaymentMap : IEntityTypeConfiguration<InvoicePayment>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<InvoicePayment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("InvoicePaymentId");

            builder.Property(x => x.InvoiceId)
                .HasColumnOrder(1);

            builder.Property(x => x.PaymentId)
                .HasColumnOrder(2);

            builder.Property(x => x.UserId)
                .HasColumnOrder(3);

            builder.ToTable(nameof(InvoicePayment));

            builder.HasOne(x => x.Invoice)
                .WithMany(x => x.InvoicePayments)
                .HasForeignKey(x => x.InvoiceId);

            builder.HasOne(x => x.Payment)
                .WithMany()
                .HasForeignKey(x => x.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                .WithMany(x=>x.InvoicePayments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
