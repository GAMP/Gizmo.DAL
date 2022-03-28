using GizmoDALV2.Entities;
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

            Property(t => t.RefundMethodId)
                .HasColumnOrder(4);

            HasRequired(t => t.Payment)
                .WithMany()
                .HasForeignKey(t => t.PaymentId);

            HasRequired(t => t.RefundMethod)
                .WithMany()
                .HasForeignKey(t => t.RefundMethodId)
                .WillCascadeOnDelete(false);

            HasOptional(t => t.Shift)
                .WithMany(t => t.Refunds)
                .WillCascadeOnDelete(false);


            // Table & Column Mappings
            ToTable(nameof(Refund));
        }
    }   
}
