using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class PointTransactionMap : EntityTypeConfiguration<PointTransaction>
    {
        public PointTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("PointTransactionId");

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

            // Table & Column Mappings
            this.ToTable(nameof(PointTransaction));

            this.HasRequired(x => x.User)
                .WithMany(x => x.LoayalityPoints)
                .HasForeignKey(x => x.UserId);       

            this.HasOptional(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById);

            this.HasOptional(x => x.ModifiedBy)
                .WithMany()
                .HasForeignKey(x => x.ModifiedById);
        }
    }
}
