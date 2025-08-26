using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <inheritdoc/>
    public sealed class RefundReceiptMap : IEntityTypeConfiguration<RefundReceipt>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<RefundReceipt> builder)
        {
            builder.ToTable(nameof(RefundReceipt));

            builder.HasKey(refundReceipt => refundReceipt.Id);

            builder.Property(refundReceipt => refundReceipt.Id)
                .HasColumnName("RefundId")
                .HasColumnOrder(0)
                .ValueGeneratedNever();

            builder.Property(refundReceipt => refundReceipt.RRN)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired(false);

            builder.HasOne(refundReceipt => refundReceipt.Refund)
                .WithOne(refund => refund.Receipt)
                .HasForeignKey<RefundReceipt>(refundReceipt => refundReceipt.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
