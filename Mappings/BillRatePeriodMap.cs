using GizmoDALV2.Entities;
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
    public class BillRatePeriodDayMap : EntityTypeConfiguration<BillRatePeriodDay>
    {
        public BillRatePeriodDayMap()
        {
            // Key
            this.HasKey(x => x.Id);

            // Table & Column Mappings
            this.ToTable("BillRatePeriodDay");

            this.Property(x => x.Id)
                .HasColumnName("BillRatePeriodDayId");

            this.Property(x => x.BillRateId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_BillRatePeriodDay") { IsUnique = true, Order = 0 } }));

            this.Property(x => x.Day)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_BillRatePeriodDay") { IsUnique = true, Order = 1 } }));

            this.HasRequired(x => x.BillRate)
                .WithMany(x => x.Days)
                .HasForeignKey(x => x.BillRateId);
        }
    }

    public class BillRatePeriodTimeMap : EntityTypeConfiguration<BillRatePeriodDayTime>
    {
        public BillRatePeriodTimeMap()
        {
            // Key
            this.HasKey(x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });

            // Table & Column Mappings
            this.ToTable("BillRatePeriodDayTime");

            this.HasRequired(x => x.Day)
                .WithMany(x => x.Times)
                .HasForeignKey(x => x.PeriodDayId);
        }
    }
}
