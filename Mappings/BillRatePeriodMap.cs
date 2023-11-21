using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class BillRatePeriodDayMap : EntityTypeConfiguration<BillRatePeriodDay>
    {
        public BillRatePeriodDayMap()
        {
            // Key
            HasKey(x => x.Id);

            // Table & Column Mappings
            ToTable("BillRatePeriodDay");

            Property(x => x.Id)
                .HasColumnName("BillRatePeriodDayId");

            Property(x => x.BillRateId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_BillRatePeriodDay") { IsUnique = true, Order = 0 } }));

            Property(x => x.Day)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_BillRatePeriodDay") { IsUnique = true, Order = 1 } }));

            HasRequired(x => x.BillRate)
                .WithMany(x => x.Days)
                .HasForeignKey(x => x.BillRateId);
        }
    }

    public class BillRatePeriodTimeMap : EntityTypeConfiguration<BillRatePeriodDayTime>
    {
        public BillRatePeriodTimeMap()
        {
            // Key
            HasKey(x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });

            // Table & Column Mappings
            ToTable("BillRatePeriodDayTime");

            HasRequired(x => x.Day)
                .WithMany(x => x.Times)
                .HasForeignKey(x => x.PeriodDayId);
        }
    }
}
