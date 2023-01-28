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
    public class BillProfileRateStepMap : EntityTypeConfiguration<BillRateStep>
    {
        public BillProfileRateStepMap()
        {
            // Primary Key
            this.HasKey(x => x.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(x => x.BillRateId)
                .IsRequired()
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_BillRateMinute") { IsUnique = true, Order = 0 } }));

            this.Property(x => x.Minute)
                .HasColumnOrder(2)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_BillRateMinute") { IsUnique = true, Order = 1 } }));

            this.Property(x => x.Action)
                .HasColumnOrder(3)
                .IsRequired();     

            this.Property(x => x.Charge)
                .HasColumnOrder(4);

            this.Property(x => x.Rate)
                .HasColumnOrder(5);

            this.Property(x => x.TargetMinute)
                .HasColumnOrder(6);

            this.ToTable("BillRateStep");

            this.Property(x => x.Id)
                .HasColumnName("BillRateStepId");

            // Relations
            this.HasRequired(x => x.BillRate)
                .WithMany(x => x.BillRateSteps)
                .HasForeignKey(x => x.BillRateId);
        }
    }
}
