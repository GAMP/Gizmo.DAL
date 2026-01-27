using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Refund entity map.
    /// </summary>
    public class RefundMap : IEntityTypeConfiguration<Refund>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            builder.ToTable(nameof(Refund));

            builder.HasKey(refund => refund.Id);

            builder.Property(refund => refund.Id)
                .HasColumnName("RefundId")
                .HasColumnOrder(0);

            builder.Property(refund => refund.PaymentId)
                .HasColumnOrder(1);

            builder.Property(refund => refund.Amount)
                .HasColumnOrder(2);

            builder.Property(refund => refund.DepositTransactionId)
                .HasColumnOrder(3);

            builder.Property(refund => refund.PointTransactionId)
                .HasColumnOrder(4);

            builder.Property(refund => refund.RefundMethodId)
                .HasColumnOrder(5);

            builder.Property(refund => refund.PaymentReversalStatus)
                .IsRequired(true)
                .HasColumnOrder(6);

            builder.Property(refund => refund.Note)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired(false)
                .HasColumnOrder(7);

            builder.HasOne(refund => refund.Payment)
                .WithMany()
                .HasForeignKey(refund => refund.PaymentId);

            builder.HasOne(refund => refund.DepositTransaction)
                .WithMany()
                .HasForeignKey(refund => refund.DepositTransactionId);

            builder.HasOne(refund => refund.PointTransaction)
                .WithMany()
                .HasForeignKey(refund => refund.PointTransactionId);

            builder.HasOne(refund => refund.Shift)
                .WithMany(shift => shift.Refunds)
                .HasForeignKey(refund => refund.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(refund => refund.RefundMethod)
                .WithMany()
                .HasForeignKey(refund => refund.RefundMethodId)
                .OnDelete(DeleteBehavior.Restrict);           
        }
    }
}
