using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class BillProfileRateStepMap : EntityTypeConfiguration<BillRateStep>
    {
        public BillProfileRateStepMap()
        {
            // Primary Key
            HasKey(x => x.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.BillRateId)
                .IsRequired()
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_BillRateMinute") { IsUnique = true, Order = 0 } }));

            Property(x => x.Minute)
                .HasColumnOrder(2)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_BillRateMinute") { IsUnique = true, Order = 1 } }));

            Property(x => x.Action)
                .HasColumnOrder(3)
                .IsRequired();

            Property(x => x.Charge)
                .HasColumnOrder(4);

            Property(x => x.Rate)
                .HasColumnOrder(5);

            Property(x => x.TargetMinute)
                .HasColumnOrder(6);

            ToTable("BillRateStep");

            Property(x => x.Id)
                .HasColumnName("BillRateStepId");

            // Relations
            HasRequired(x => x.BillRate)
                .WithMany(x => x.BillRateSteps)
                .HasForeignKey(x => x.BillRateId);
        }
    }
}
