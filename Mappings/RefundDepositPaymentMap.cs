using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class RefundDepositPaymentMap : IEntityTypeConfiguration<RefundDepositPayment>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<RefundDepositPayment> builder)
        {
            builder.Property(t => t.DepositPaymentId)
                .HasColumnOrder(1);

            builder.Property(x => x.FiscalReceiptStatus)
                .HasColumnOrder(2);

            builder.Property(x => x.FiscalReceiptId)
                .HasColumnOrder(3);

            // Indexes
            builder.HasIndex(t => t.DepositPaymentId).HasDatabaseName("UQ_DepositPayment").IsUnique();
            builder.HasIndex(t => t.Id);

            builder.HasOne(t => t.DepositPayment)
                .WithMany()
                .HasForeignKey(x=>x.DepositPaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.FiscalReceipt)
                .WithMany()
                .HasForeignKey(x => x.FiscalReceiptId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(nameof(RefundDepositPayment));
        }
    }
}
