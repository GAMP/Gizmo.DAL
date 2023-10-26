using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class DepositTransactionMap : EntityTypeConfiguration<DepositTransaction>
    {
        public DepositTransactionMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            Property(x => x.Id)
                .HasColumnName("DepositTransactionId")
                .HasColumnOrder(0);

            Property(x => x.UserId)
                .HasColumnOrder(1);

            Property(x => x.Type)
                .HasColumnOrder(2);

            Property(x => x.Amount)
                .HasColumnOrder(3);

            Property(x => x.Balance)
                .HasColumnOrder(4);

            Property(x => x.IsVoided)
                .HasColumnOrder(5);

            ToTable(nameof(DepositTransaction));

            // Relationships
            HasRequired(x => x.User)
                .WithMany(x => x.Deposits)
                .HasForeignKey(x => x.UserId);

            HasOptional(x => x.CreatedBy)
                .WithMany(x => x.CreatedDeposits)
                .HasForeignKey(x => x.CreatedById);

            HasOptional(x => x.ModifiedBy)
              .WithMany(x => x.ModifiedDeposits)
              .HasForeignKey(x => x.ModifiedById);
        }
    }
}
