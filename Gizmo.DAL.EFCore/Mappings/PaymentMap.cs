using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class PaymentMap : IEntityTypeConfiguration<Payment>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.ToTable("Payment");

            builder.Property(x => x.Id)
                .HasColumnName("PaymentId")
                .HasColumnOrder(0);

            builder.Property(x => x.UserId)
                .HasColumnOrder(1);

            builder.Property(x => x.PaymentMethodId)
                .HasColumnOrder(2);

            builder.Property(x => x.Amount)
                .HasColumnOrder(3);

            builder.Property(x => x.AmountReceived)
                .HasColumnOrder(4);

            builder.Property(x => x.IsDeleted)
                .HasColumnOrder(5);

            //builder.Property(x => x.IsRefunded)
            //    .HasColumnOrder(6);

            builder.Property(x => x.IsVoided)
                .HasColumnOrder(7);

            builder.Property(x => x.DepositTransactionId)
                .HasColumnOrder(8);

            builder.Property(x => x.PointTransactionId)
                .HasColumnOrder(9);

            // Indexes
            builder.HasIndex(t => t.DepositTransactionId).HasDatabaseName("UQ_DepositTransaction").IsUnique();
            builder.HasIndex(t => t.PointTransactionId).HasDatabaseName("UQ_PointsTransaction").IsUnique();

            // Relationships
            builder.HasOne(x => x.User)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.PaymentMethod)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.PaymentMethodId);

            builder.HasOne(x => x.CreatedBy)
                .WithMany(x => x.CreatedPayments)
                .HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.ModifiedBy)
                .WithMany(x => x.ModifiedPayments)
                .HasForeignKey(x => x.ModifiedById);

            builder.HasOne(x => x.PointTransaction)
                .WithMany()
                .HasForeignKey(x => x.PointTransactionId);

            builder.HasOne(x => x.DepositTransaction)
                .WithMany()
                .HasForeignKey(x => x.DepositTransactionId);
        }
    }
}
