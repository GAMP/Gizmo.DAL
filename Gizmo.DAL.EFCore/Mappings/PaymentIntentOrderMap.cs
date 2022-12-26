using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Order payment intent map.
    /// </summary>
    public class PaymentIntentOrderMap : IEntityTypeConfiguration<PaymentIntentOrder>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<PaymentIntentOrder> builder)
        {
            builder.Property(x => x.ProductOrderId)
                //.IsRequired(false)
                .HasColumnOrder(1);

            builder.Property(x => x.InvoicePaymentId)
                .IsRequired(false)
                .HasColumnOrder(2);

            // Indexes
            builder.HasIndex(t => t.InvoicePaymentId).HasDatabaseName("UQ_InvoicePayment").IsUnique(); //same invoice payment may not appear multiple times
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.ProductOrder)
                .WithMany(x => x.PaymentIntents)
                .HasForeignKey(x => x.ProductOrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.InvoicePayment)
                .WithMany()
                .HasForeignKey(x => x.InvoicePaymentId);

            builder.ToTable(nameof(PaymentIntentOrder));
        }
    }
}
