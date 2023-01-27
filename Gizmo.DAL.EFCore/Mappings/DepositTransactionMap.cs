using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class DepositTransactionMap : IEntityTypeConfiguration<DepositTransaction>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<DepositTransaction> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            builder.Property(x => x.Id)
                .HasColumnName("DepositTransactionId")
                .HasColumnOrder(0);

            builder.Property(x => x.UserId)
                .HasColumnOrder(1);

            builder.Property(x => x.Type)
                .HasColumnOrder(2);

            builder.Property(x => x.Amount)
                .HasColumnOrder(3);

            builder.Property(x => x.Balance)
                .HasColumnOrder(4);

            builder.Property(x => x.IsVoided)
                .HasColumnOrder(5);

            builder.ToTable(nameof(DepositTransaction));

            // Relationships
            builder.HasOne(x => x.User)
                .WithMany(x => x.Deposits)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.CreatedBy)
                .WithMany(x => x.CreatedDeposits)
                .HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.ModifiedBy)
              .WithMany(x => x.ModifiedDeposits)
              .HasForeignKey(x => x.ModifiedById);
        }
    }
}
