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
                .HasColumnName("PaymentReceiptId")
                .HasColumnOrder(0);

            builder.Property(paymentReceipt => paymentReceipt.PaymentId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(paymentReceipt => paymentReceipt.Type)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(paymentReceipt => paymentReceipt.RRN)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired(false)
                .HasColumnOrder(3);

            builder.Property(paymentReceipt => paymentReceipt.CompanionId)
                .IsRequired(false)
                .HasColumnOrder(4);

            builder.Property(paymentReceipt => paymentReceipt.TerminalNumber)
                .IsRequired(false)
                .HasColumnOrder(5);

            builder.HasIndex(paymentReceipt => paymentReceipt.PaymentId);

            builder.HasOne(paymentReceipt => paymentReceipt.Payment)
                .WithMany(payment => payment.Receipts)
                .HasForeignKey(paymentReceipt => paymentReceipt.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(paymentReceipt => paymentReceipt.Companion)
                .WithMany(companion => companion.PaymentReceipts)
                .HasForeignKey(paymentReceipt => paymentReceipt.CompanionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
