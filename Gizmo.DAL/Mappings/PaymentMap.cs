using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Payment entity map.
    /// </summary>
    public class PaymentMap : IEntityTypeConfiguration<Payment>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment");

            builder.HasKey(payment => payment.Id);

            builder.Property(payment => payment.Id)
                .HasColumnName("PaymentId")
                .HasColumnOrder(0);

            builder.Property(payment => payment.UserId)
                .HasColumnOrder(1);

            builder.Property(payment => payment.PaymentMethodId)
                .HasColumnOrder(2);

            builder.Property(payment => payment.Amount)
                .HasColumnOrder(3);

            builder.Property(payment => payment.AmountReceived)
                .HasColumnOrder(4);

            builder.Property(payment => payment.IsDeleted)
                .HasColumnOrder(5);

            builder.Property(payment => payment.IsVoided)
                .HasColumnOrder(7);

            builder.Property(payment => payment.DepositTransactionId)
                .IsRequired(false)
                .HasColumnOrder(8);

            builder.Property(payment => payment.PointTransactionId)
                .IsRequired(false)
                .HasColumnOrder(9);

            // Indexes
            builder.HasIndex(payment => payment.DepositTransactionId)
                .IsUnique();

            builder.HasIndex(payment => payment.PointTransactionId)
                .IsUnique();

            // Relationships
            builder.HasOne(payment => payment.User)
                .WithMany(userMember => userMember.Payments)
                .HasForeignKey(payment => payment.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(payment => payment.PaymentMethod)
                .WithMany(paymentMethod => paymentMethod.Payments)
                .HasForeignKey(payment => payment.PaymentMethodId);

            builder.HasOne(payment => payment.CreatedBy)
                .WithMany(userOperator => userOperator.CreatedPayments)
                .HasForeignKey(payment => payment.CreatedById);

            builder.HasOne(payment => payment.ModifiedBy)
                .WithMany(userOperator => userOperator.ModifiedPayments)
                .HasForeignKey(payment => payment.ModifiedById);

            builder.HasOne(payment => payment.PointTransaction)
                .WithMany()
                .HasForeignKey(payment => payment.PointTransactionId);

            builder.HasOne(payment => payment.DepositTransaction)
                .WithMany()
                .HasForeignKey(payment => payment.DepositTransactionId);
        }
    }
}
