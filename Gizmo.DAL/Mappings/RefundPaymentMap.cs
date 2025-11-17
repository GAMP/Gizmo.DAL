using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Refund payment map.
    /// </summary>
    public sealed class RefundPaymentMap : IEntityTypeConfiguration<RefundPayment>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<RefundPayment> builder)
        {
            builder.ToTable(nameof(RefundPayment));
            builder.HasBaseType<Refund>();

            builder.Property(refundPayment => refundPayment.FiscalReceiptStatus)
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(refundPayment => refundPayment.FiscalReceiptId)
                .HasColumnOrder(1)
                .IsRequired(false);
        }
    }
}
