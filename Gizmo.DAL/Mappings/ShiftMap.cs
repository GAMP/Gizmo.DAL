using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class ShiftMap : EntityTypeConfiguration<Shift>
    {
        public ShiftMap()
        {
            this.ToTable(nameof(Shift));

            this.HasKey(x => x.Id);

            this.Property(x => x.Id)
                .HasColumnName("ShiftId")
                .HasColumnOrder(0);

            this.Property(x => x.IsActive)
                .HasColumnOrder(1);

            this.Property(x => x.OperatorId)
                .HasColumnOrder(2);

            this.Property(x => x.RegisterId)
                .HasColumnOrder(3);

            this.Property(x => x.Start)
                .HasColumnOrder(4);

            this.Property(x => x.StartCash)
                .HasColumnOrder(5);

            this.Property(x => x.IsEnding)
                .HasColumnOrder(6);

            this.Property(x => x.EndedById)
                .IsOptional()
                .HasColumnOrder(7);

            this.Property(x => x.EndTime)
                .IsOptional()
                .HasColumnOrder(8);

            this.HasOptional(x => x.EndedBy)
                .WithMany()
                .HasForeignKey(x => x.EndedById);

            this.HasRequired(x => x.Register)
                .WithMany(x => x.Shifts)
                .HasForeignKey(x => x.RegisterId);

            this.HasRequired(x => x.Operator)
                .WithMany(x => x.Shifts)
                .HasForeignKey(x => x.OperatorId);
        }
    }
}
