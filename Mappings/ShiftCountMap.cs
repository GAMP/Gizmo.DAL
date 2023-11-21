using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ShiftCountMap : EntityTypeConfiguration<ShiftCount>
    {
        public ShiftCountMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("ShiftCountId")
                .HasColumnOrder(0);

            Property(x => x.ShiftId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ShiftCountPaymentMethod")
                {
                    IsUnique = true,
                    Order = 0
                } }));

            Property(x => x.PaymentMethodId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ShiftCountPaymentMethod")
                {
                    IsUnique = true,
                    Order = 1
                } }));

            Property(x => x.StartCash)
                .HasColumnOrder(3);

            Property(x => x.Sales)
                .HasColumnOrder(4);

            Property(x => x.Deposits)
                .HasColumnOrder(5);

            Property(x => x.PayIns)
                .HasColumnOrder(6);

            Property(x => x.Withdrawals)
                .HasColumnOrder(7);

            Property(x => x.PayOuts)
                .HasColumnOrder(8);

            Property(x => x.Refunds)
               .HasColumnOrder(9);

            Property(x => x.Voids)
               .HasColumnOrder(10);

            Property(x => x.Expected)
                .HasColumnOrder(11);

            Property(x => x.Actual)
                .HasColumnOrder(12);

            Property(x => x.Difference)
                .HasColumnOrder(13);

            Property(x => x.Note)
                .HasColumnOrder(14);

            // Table & Column Mappings
            ToTable(nameof(ShiftCount));

            // Relationships
            HasRequired(x => x.Shift)
                .WithMany(x => x.ShiftCounts)
                .HasForeignKey(x => x.ShiftId)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.PaymentMethod)
                .WithMany()
                .HasForeignKey(x => x.PaymentMethodId)
                .WillCascadeOnDelete(false);
        }
    }
}
