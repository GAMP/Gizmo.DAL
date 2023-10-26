using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class RegisterTransactionMap : EntityTypeConfiguration<RegisterTransaction>
    {
        public RegisterTransactionMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("RegisterTransactionId")
                .HasColumnOrder(0);

            Property(x => x.RegisterId)
                .HasColumnOrder(1);

            Property(x => x.ShiftId)
                .HasColumnOrder(2);

            Property(x => x.Amount)
                .HasColumnOrder(3);

            Property(x => x.Type)
                .HasColumnOrder(4);

            Property(x => x.Note)
                .HasColumnOrder(5);

            // Table & Column Mappings
            ToTable(nameof(RegisterTransaction));

            // Relationships
            HasRequired(x => x.Register)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.RegisterId);

            HasOptional(x => x.Shift)
                .WithMany(x => x.RegisterTransactions)
                .HasForeignKey(x => x.ShiftId);

            HasOptional(x => x.CreatedBy)
                .WithMany(x => x.RegisterTransactions)
                .HasForeignKey(x => x.CreatedById);
        }
    }
}
