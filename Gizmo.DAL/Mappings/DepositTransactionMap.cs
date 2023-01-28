using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class DepositTransactionMap : EntityTypeConfiguration<DepositTransaction>
    {
        public DepositTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(x => x.Id)
                .HasColumnName("DepositTransactionId")
                .HasColumnOrder(0);

            this.Property(x => x.UserId)
                .HasColumnOrder(1);

            this.Property(x => x.Type)
                .HasColumnOrder(2);

            this.Property(x => x.Amount)
                .HasColumnOrder(3);

            this.Property(x => x.Balance)
                .HasColumnOrder(4);

            this.Property(x => x.IsVoided)
                .HasColumnOrder(5);

            this.ToTable(nameof(DepositTransaction));

            // Relationships
            this.HasRequired(x => x.User)
                .WithMany(x => x.Deposits)
                .HasForeignKey(x => x.UserId);

            this.HasOptional(x => x.CreatedBy)
                .WithMany(x => x.CreatedDeposits)
                .HasForeignKey(x => x.CreatedById);

            this.HasOptional(x => x.ModifiedBy)
              .WithMany(x => x.ModifiedDeposits)
              .HasForeignKey(x => x.ModifiedById);
        }
    }
}
