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

            // payment receipt have one - to - one mapping to payment
            // multiple receipts are possible with same or different types

            builder.Property(paymentReceipt => paymentReceipt.Id)
                .HasColumnName("PaymentId")
                .HasColumnOrder(0)
                .ValueGeneratedNever();

            builder.Property(paymentReceipt => paymentReceipt.Type)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(paymentReceipt => paymentReceipt.RRN)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired(false)
                .HasColumnOrder(2);

            builder.Property(paymentReceipt => paymentReceipt.CompanionId)
                .IsRequired(false)
                .HasColumnOrder(3);

            builder.Property(paymentReceipt => paymentReceipt.TerminalNumber)
                .IsRequired(false)
                .HasColumnOrder(4);

            builder.HasOne(paymentReceipt => paymentReceipt.Payment)
                .WithOne(payment => payment.Receipt)
                .HasForeignKey<PaymentReceipt>(paymentReceipt => paymentReceipt.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(paymentReceipt => paymentReceipt.Companion)
                .WithMany(companion => companion.PaymentReceipts)
                .HasForeignKey(paymentReceipt => paymentReceipt.CompanionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
