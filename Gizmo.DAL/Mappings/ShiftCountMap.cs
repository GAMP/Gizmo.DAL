using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class ShiftCountMap : IEntityTypeConfiguration<ShiftCount>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ShiftCount> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("ShiftCountId")
                .HasColumnOrder(0);

            builder.Property(x => x.ShiftId)
                .HasColumnOrder(1);

            builder.Property(x => x.PaymentMethodId)
                .HasColumnOrder(2);

            builder.Property(x => x.StartCash)
                .HasColumnOrder(3);

            builder.Property(x => x.Sales)
                .HasColumnOrder(4);

            builder.Property(x => x.Deposits)
                .HasColumnOrder(5);

            builder.Property(x => x.PayIns)
                .HasColumnOrder(6);

            builder.Property(x => x.Withdrawals)
                .HasColumnOrder(7);

            builder.Property(x => x.PayOuts)
                .HasColumnOrder(8);

            builder.Property(x => x.Refunds)
               .HasColumnOrder(9);

            builder.Property(x => x.Voids)
               .HasColumnOrder(10);

            builder.Property(x => x.Expected)
                .HasColumnOrder(11);

            builder.Property(x => x.Actual)
                .HasColumnOrder(12);

            builder.Property(x => x.Difference)
                .HasColumnOrder(13);

            builder.Property(x => x.Note)
                .HasColumnOrder(14);

            // Indexes
            builder.HasIndex(x => new { x.ShiftId, x.PaymentMethodId }).IsUnique();

            // Table & Column Mappings
            builder.ToTable(nameof(ShiftCount));

            // Relationships
            builder.HasOne(x => x.Shift)
                .WithMany(x => x.ShiftCounts)
                .HasForeignKey(x => x.ShiftId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.PaymentMethod)
                .WithMany()
                .HasForeignKey(x => x.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
