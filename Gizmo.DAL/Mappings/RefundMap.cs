using Gizmo.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class RefundMap : EntityTypeConfiguration<Refund>
    {
        public RefundMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("RefundId")
                .HasColumnOrder(0);

            Property(t => t.PaymentId)
                .HasColumnOrder(1);

            Property(t => t.Amount)
                .HasColumnOrder(2);

            Property(t => t.DepositTransactionId)
                .HasColumnOrder(3);

            Property(t => t.PointTransactionId)
                .HasColumnOrder(4);

            Property(t => t.RefundMethodId)
                .HasColumnOrder(5);

            HasOptional(t => t.Payment)
                .WithMany()
                .HasForeignKey(t => t.PaymentId);

            HasOptional(t => t.DepositTransaction)
                .WithMany()
                .HasForeignKey(t => t.DepositTransactionId);

            HasOptional(t => t.PointTransaction)
                .WithMany()
                .HasForeignKey(t => t.PointTransactionId);

            HasOptional(t => t.Shift)
                .WithMany(t => t.Refunds)
                .HasForeignKey(t => t.ShiftId)
                .WillCascadeOnDelete(false);

            HasRequired(t => t.RefundMethod)
                .WithMany()
                .HasForeignKey(t => t.RefundMethodId)
                .WillCascadeOnDelete(false);

            // Table & Column Mappings
            ToTable(nameof(Refund));
        }
    }
}
