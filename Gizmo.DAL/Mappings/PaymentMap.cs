using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class PaymentMap : EntityTypeConfiguration<Payment>
    {
        public PaymentMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            ToTable("Payment");

            Property(x => x.Id)
                .HasColumnName("PaymentId")
                .HasColumnOrder(0);

            Property(x => x.UserId)
                .HasColumnOrder(1);

            Property(x => x.PaymentMethodId)
                .HasColumnOrder(2);

            Property(x => x.Amount)
                .HasColumnOrder(3);

            Property(x => x.AmountReceived)
                .HasColumnOrder(4);

            Property(x => x.IsDeleted)
                .HasColumnOrder(5);

            //Property(x => x.IsRefunded)
            //    .HasColumnOrder(6);

            Property(x => x.IsVoided)
                .HasColumnOrder(7);

            Property(x => x.DepositTransactionId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_DepositTransaction")
                {
                    IsUnique = true
                }}))
                .HasColumnOrder(8);

            Property(x => x.PointTransactionId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_PointsTransaction")
                {
                    IsUnique = true
                }}))
                .HasColumnOrder(9);

            // Relationships
            HasRequired(x => x.User)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.UserId);

            HasRequired(x => x.PaymentMethod)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.PaymentMethodId);

            HasOptional(x => x.CreatedBy)
                .WithMany(x => x.CreatedPayments)
                .HasForeignKey(x => x.CreatedById);

            HasOptional(x => x.ModifiedBy)
                .WithMany(x => x.ModifiedPayments)
                .HasForeignKey(x => x.ModifiedById);

            HasOptional(x => x.PointTransaction)
                .WithMany()
                .HasForeignKey(x => x.PointTransactionId);

            HasOptional(x => x.DepositTransaction)
                .WithMany()
                .HasForeignKey(x => x.DepositTransactionId);
        }
    }
}
