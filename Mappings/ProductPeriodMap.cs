using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ProductPeriodMap : EntityTypeConfiguration<ProductPeriod>
    {
        public ProductPeriodMap()
        {
            // Key
            HasKey(x => x.Id);

            // Properties
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("ProductPeriod");

            Property(x => x.Id)
                .HasColumnName("ProductId");

            HasRequired(x => x.Product)
                .WithRequiredDependent(x => x.Period)
                .WillCascadeOnDelete(true);
        }
    }

    public class ProductPeriodDayMap : EntityTypeConfiguration<ProductPeriodDay>
    {
        public ProductPeriodDayMap()
        {
            // Key
            HasKey(x => x.Id);

            // Table & Column Mappings
            ToTable("ProductPeriodDay");

            Property(x => x.Id)
                .HasColumnName("ProductPeriodDayId");

            Property(x => x.ProductPeriodId)
                .HasColumnName("ProductPeriodId")
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductPeriodDay") { IsUnique = true, Order = 0 } }));

            Property(x=> x.Day)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductPeriodDay") { IsUnique = true, Order = 1 } }));

            HasRequired(x => x.Period)
                .WithMany(x => x.Days)
                .HasForeignKey(x => x.ProductPeriodId);
        }
    }

    public class ProductPeriodDayTimeMap : EntityTypeConfiguration<ProductPeriodDayTime>
    {
        public ProductPeriodDayTimeMap()
        {
            // Key
            HasKey(x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });

            // Table & Column Mappings
            ToTable("ProductPeriodDayTime");

            HasRequired(x => x.Day)
                .WithMany(x => x.Times)
                .HasForeignKey(x => x.PeriodDayId);
        }
    }
}
