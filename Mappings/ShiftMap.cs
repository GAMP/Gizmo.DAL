using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ShiftMap : EntityTypeConfiguration<Shift>
    {
        public ShiftMap()
        {
            ToTable(nameof(Shift));

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("ShiftId")
                .HasColumnOrder(0);

            Property(x => x.IsActive)
                .HasColumnOrder(1);

            Property(x => x.OperatorId)
                .HasColumnOrder(2);

            Property(x => x.RegisterId)
                .HasColumnOrder(3);

            Property(x => x.Start)
                .HasColumnOrder(4);   

            Property(x => x.StartCash)
                .HasColumnOrder(5);

            Property(x => x.IsEnding)
                .HasColumnOrder(6);

            Property(x => x.EndedById)
                .IsOptional()
                .HasColumnOrder(7);

            Property(x => x.EndTime)
                .IsOptional()
                .HasColumnOrder(8);

            HasOptional(x => x.EndedBy)
                .WithMany()
                .HasForeignKey(x => x.EndedById);

            HasRequired(x => x.Register)
                .WithMany(x => x.Shifts)
                .HasForeignKey(x => x.RegisterId);

            HasRequired(x => x.Operator)
                .WithMany(x=>x.Shifts)
                .HasForeignKey(x => x.OperatorId);
        }
    }
}
