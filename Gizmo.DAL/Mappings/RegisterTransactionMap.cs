using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class RegisterTransactionMap : EntityTypeConfiguration<RegisterTransaction>
    {
        public RegisterTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("RegisterTransactionId")
                .HasColumnOrder(0);

            this.Property(x => x.RegisterId)
                .HasColumnOrder(1);

            this.Property(x => x.ShiftId)
                .HasColumnOrder(2) ;

            this.Property(x => x.Amount)
                .HasColumnOrder(3);

            this.Property(x => x.Type)
                .HasColumnOrder(4);

            this.Property(x => x.Note)
                .HasColumnOrder(5);

            // Table & Column Mappings
            this.ToTable(nameof(RegisterTransaction));

            // Relationships
            this.HasRequired(x => x.Register)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.RegisterId);

            this.HasOptional(x => x.Shift)
                .WithMany(x=>x.RegisterTransactions)
                .HasForeignKey(x => x.ShiftId);

            this.HasOptional(x => x.CreatedBy)
                .WithMany(x => x.RegisterTransactions)
                .HasForeignKey(x => x.CreatedById);
        }
    }
}
