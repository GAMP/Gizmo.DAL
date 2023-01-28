using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace GizmoDALV2.Mappings
{ 
    public class ProductTimePeriodMap : EntityTypeConfiguration<ProductTimePeriod>
    {
        public ProductTimePeriodMap()
        {
            // Key
            this.HasKey(x => x.Id);

            // Properties
            this.Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductTimePeriod");

            this.Property(x => x.Id)
                .HasColumnName("ProductId");

            this.HasRequired(x => x.ProductTime)
                .WithRequiredDependent(x => x.UsePeriod)
                .WillCascadeOnDelete(true);
        }
    }

    public class ProductTimePeriodDayMap : EntityTypeConfiguration<ProductTimePeriodDay>
    {
        public ProductTimePeriodDayMap()
        {
            // Key
            this.HasKey(x => x.Id);

            // Table & Column Mappings
            this.ToTable("ProductTimePeriodDay");

            this.Property(x => x.Id)
                .HasColumnName("ProductTimePeriodDayId");

            this.Property(x => x.ProductTimePeriodId)
                .HasColumnName("ProductTimePeriodId")
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductTimePeriodDay") { IsUnique = true, Order = 0 } }));

            this.Property(x=> x.Day)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductTimePeriodDay") { IsUnique = true, Order = 1 } }));

            this.HasRequired(x => x.Period)
                .WithMany(x => x.Days)
                .HasForeignKey(x => x.ProductTimePeriodId);
        }
    }

    public class ProductTimePeriodDayTimeMap : EntityTypeConfiguration<ProductTimePeriodDayTime>
    {
        public ProductTimePeriodDayTimeMap()
        {
            // Key
            this.HasKey(x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });

            // Table & Column Mappings
            this.ToTable("ProductTimePeriodDayTime");

            this.HasRequired(x => x.Day)
                .WithMany(x => x.Times)
                .HasForeignKey(x => x.PeriodDayId);
        }
    }
}
