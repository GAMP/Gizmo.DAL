using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <inheritdoc/>
    public sealed class PaymentReceiptMap : IEntityTypeConfiguration<PaymentReceipt>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PaymentReceipt> builder)
        {
            builder.ToTable(nameof(PaymentReceipt));

            builder.HasKey(paymentReceipt => paymentReceipt.Id);

            builder.Property(paymentReceipt => paymentReceipt.Id)
                .HasColumnName("PaymentId")
                .HasColumnOrder(0)
                .ValueGeneratedNever();

            builder.Property(paymentReceipt => paymentReceipt.RRN)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired(false);

            builder.HasOne(paymentReceipt => paymentReceipt.Payment)
                .WithOne(payment => payment.Receipt)
                .HasForeignKey<PaymentReceipt>(paymentReceipt => paymentReceipt.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
