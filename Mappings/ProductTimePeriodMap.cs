using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ProductTimePeriodMap : EntityTypeConfiguration<ProductTimePeriod>
    {
        public ProductTimePeriodMap()
        {
            // Key
            HasKey(x => x.Id);

            // Properties
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("ProductTimePeriod");

            Property(x => x.Id)
                .HasColumnName("ProductId");

            HasRequired(x => x.ProductTime)
                .WithRequiredDependent(x => x.UsePeriod)
                .WillCascadeOnDelete(true);
        }
    }

    public class ProductTimePeriodDayMap : EntityTypeConfiguration<ProductTimePeriodDay>
    {
        public ProductTimePeriodDayMap()
        {
            // Key
            HasKey(x => x.Id);

            // Table & Column Mappings
            ToTable("ProductTimePeriodDay");

            Property(x => x.Id)
                .HasColumnName("ProductTimePeriodDayId");

            Property(x => x.ProductTimePeriodId)
                .HasColumnName("ProductTimePeriodId")
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductTimePeriodDay") { IsUnique = true, Order = 0 } }));

            Property(x=> x.Day)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductTimePeriodDay") { IsUnique = true, Order = 1 } }));

            HasRequired(x => x.Period)
                .WithMany(x => x.Days)
                .HasForeignKey(x => x.ProductTimePeriodId);
        }
    }

    public class ProductTimePeriodDayTimeMap : EntityTypeConfiguration<ProductTimePeriodDayTime>
    {
        public ProductTimePeriodDayTimeMap()
        {
            // Key
            HasKey(x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });

            // Table & Column Mappings
            ToTable("ProductTimePeriodDayTime");

            HasRequired(x => x.Day)
                .WithMany(x => x.Times)
                .HasForeignKey(x => x.PeriodDayId);
        }
    }
}
