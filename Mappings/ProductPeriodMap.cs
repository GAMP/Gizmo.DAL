using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace GizmoDALV2.Mappings
{ 
    public class ProductPeriodMap : EntityTypeConfiguration<ProductPeriod>
    {
        public ProductPeriodMap()
        {
            // Key
            this.HasKey(x => x.Id);

            // Properties
            this.Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductPeriod");

            this.Property(x => x.Id)
                .HasColumnName("ProductId");

            this.HasRequired(x => x.Product)
                .WithRequiredDependent(x => x.Period)
                .WillCascadeOnDelete(true);
        }
    }

    public class ProductPeriodDayMap : EntityTypeConfiguration<ProductPeriodDay>
    {
        public ProductPeriodDayMap()
        {
            // Key
            this.HasKey(x => x.Id);

            // Table & Column Mappings
            this.ToTable("ProductPeriodDay");

            this.Property(x => x.Id)
                .HasColumnName("ProductPeriodDayId");

            this.Property(x => x.ProductPeriodId)
                .HasColumnName("ProductPeriodId")
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductPeriodDay") { IsUnique = true, Order = 0 } }));

            this.Property(x=> x.Day)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductPeriodDay") { IsUnique = true, Order = 1 } }));

            this.HasRequired(x => x.Period)
                .WithMany(x => x.Days)
                .HasForeignKey(x => x.ProductPeriodId);
        }
    }

    public class ProductPeriodDayTimeMap : EntityTypeConfiguration<ProductPeriodDayTime>
    {
        public ProductPeriodDayTimeMap()
        {
            // Key
            this.HasKey(x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });

            // Table & Column Mappings
            this.ToTable("ProductPeriodDayTime");

            this.HasRequired(x => x.Day)
                .WithMany(x => x.Times)
                .HasForeignKey(x => x.PeriodDayId);
        }
    }
}
