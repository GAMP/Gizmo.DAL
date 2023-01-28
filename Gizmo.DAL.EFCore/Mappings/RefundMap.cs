using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class RefundMap : IEntityTypeConfiguration<Refund>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnName("RefundId")
                .HasColumnOrder(0);

            builder.Property(t => t.PaymentId)
                .HasColumnOrder(1);

            builder.Property(t => t.Amount)
                .HasColumnOrder(2);

            builder.Property(t => t.DepositTransactionId)
                .HasColumnOrder(3);

            builder.Property(t => t.PointTransactionId)
                .HasColumnOrder(4);

            builder.Property(t => t.RefundMethodId)
                .HasColumnOrder(5);

            builder.HasOne(t => t.Payment)
                .WithMany()
                .HasForeignKey(t => t.PaymentId);

            builder.HasOne(t => t.DepositTransaction)
                .WithMany()
                .HasForeignKey(t => t.DepositTransactionId);

            builder.HasOne(t => t.PointTransaction)
                .WithMany()
                .HasForeignKey(t => t.PointTransactionId);

            builder.HasOne(t => t.Shift)
                .WithMany(t => t.Refunds)
                .HasForeignKey(t => t.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.RefundMethod)
                .WithMany()
                .HasForeignKey(t => t.RefundMethodId)
                .OnDelete(DeleteBehavior.Restrict);

            // Table & Column Mappings
            builder.ToTable(nameof(Refund));
        }
    }
}
