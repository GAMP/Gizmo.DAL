using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class ShiftMap : IEntityTypeConfiguration<Shift>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.ToTable(nameof(Shift));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ShiftId")
                .HasColumnOrder(0);

            builder.Property(x => x.IsActive)
                .HasColumnOrder(1);

            builder.Property(x => x.OperatorId)
                .HasColumnOrder(2);

            builder.Property(x => x.RegisterId)
                .HasColumnOrder(3);

            builder.Property(x => x.Start)
                .HasColumnOrder(4);

            builder.Property(x => x.StartCash)
                .HasColumnOrder(5);

            builder.Property(x => x.IsEnding)
                .HasColumnOrder(6);

            builder.Property(x => x.EndedById)
                .IsRequired(false)
                .HasColumnOrder(7);

            builder.Property(x => x.EndTime)
                .IsRequired(false)
                .HasColumnOrder(8);

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.Branch)
                .WithMany(x => x.Shifts)
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.EndedBy)
                .WithMany()
                .HasForeignKey(x => x.EndedById);

            builder.HasOne(x => x.Register)
                .WithMany(x => x.Shifts)
                .HasForeignKey(x => x.RegisterId);

            builder.HasOne(x => x.Operator)
                .WithMany(x => x.Shifts)
                .HasForeignKey(x => x.OperatorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById);
        }
    }
}
