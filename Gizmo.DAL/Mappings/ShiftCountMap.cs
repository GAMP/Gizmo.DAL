using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class ShiftCountMap : EntityTypeConfiguration<ShiftCount>
    {
        public ShiftCountMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("ShiftCountId")
                .HasColumnOrder(0);

            this.Property(x => x.ShiftId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ShiftCountPaymentMethod")
                {
                    IsUnique = true,
                    Order = 0
                } }));

            this.Property(x => x.PaymentMethodId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ShiftCountPaymentMethod")
                {
                    IsUnique = true,
                    Order = 1
                } }));

            this.Property(x => x.StartCash)
                .HasColumnOrder(3);

            this.Property(x => x.Sales)
                .HasColumnOrder(4);

            this.Property(x => x.Deposits)
                .HasColumnOrder(5);

            this.Property(x => x.PayIns)
                .HasColumnOrder(6);

            this.Property(x => x.Withdrawals)
                .HasColumnOrder(7);

            this.Property(x => x.PayOuts)
                .HasColumnOrder(8);

            this.Property(x => x.Refunds)
               .HasColumnOrder(9);

            this.Property(x => x.Voids)
               .HasColumnOrder(10);

            this.Property(x => x.Expected)
                .HasColumnOrder(11);

            this.Property(x => x.Actual)
                .HasColumnOrder(12);

            this.Property(x => x.Difference)
                .HasColumnOrder(13);

            this.Property(x => x.Note)
                .HasColumnOrder(14);

            // Table & Column Mappings
            this.ToTable(nameof(ShiftCount));

            // Relationships
            this.HasRequired(x => x.Shift)
                .WithMany(x => x.ShiftCounts)
                .HasForeignKey(x => x.ShiftId)
                .WillCascadeOnDelete(true);

            this.HasRequired(x => x.PaymentMethod)
                .WithMany()
                .HasForeignKey(x => x.PaymentMethodId)
                .WillCascadeOnDelete(false);
        }
    }
}
