using Gizmo.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ProductOLBaseMap : EntityTypeConfiguration<ProductOL>
    {
        public ProductOLBaseMap()
        {
            ToTable(nameof(ProductOL));

            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("ProductOLId");

            Property(x => x.ProductOrderId)
                .HasColumnOrder(1);

            Property(x => x.UserId)
                .HasColumnOrder(2);

            Property(x => x.ProductName)
                .HasColumnOrder(3);

            Property(x => x.Quantity)
                .HasColumnOrder(4);

            Property(x => x.UnitPrice)
                .HasColumnOrder(5);

            Property(x => x.UnitListPrice)
                .HasColumnOrder(6);

            Property(x => x.UnitPointsPrice)
                .HasColumnOrder(7);

            Property(x => x.UnitPointsListPrice)
                .HasColumnOrder(8);

            Property(x => x.UnitCost)
                .HasColumnOrder(9);

            Property(x => x.Cost)
                .HasColumnOrder(10);

            Property(x => x.TaxRate)
                .HasColumnOrder(11);

            Property(x => x.PreTaxTotal)
                .HasColumnOrder(12);

            Property(x => x.Total)
                .HasColumnOrder(13);

            Property(x => x.PointsTotal)
                .HasColumnOrder(14);

            Property(x => x.Points)
                .HasColumnOrder(15);

            Property(x => x.PointsAward)
                .HasColumnOrder(16);

            Property(x => x.TaxTotal)
                .HasColumnOrder(17);

            Property(x => x.PayType)
               .HasColumnOrder(18);

            Property(x => x.IsDeleted)
                .HasColumnOrder(19);

            Property(x => x.IsVoided)
                .HasColumnOrder(20);  

            HasRequired(x => x.ProductOrder)
                .WithMany(x => x.OrderLines)
                .HasForeignKey(x => x.ProductOrderId);

            HasOptional(x => x.User)
                .WithMany(x => x.ProductOrdersLines)
                .HasForeignKey(x => x.UserId);

            HasOptional(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById);

            HasOptional(x => x.ModifiedBy)
                .WithMany()
                .HasForeignKey(x => x.ModifiedById);
        }
    }

    public class ProductOLExtendedMap : EntityTypeConfiguration<ProductOLExtended>
    {
        public ProductOLExtendedMap()
        {
            ToTable(nameof(ProductOLExtended));

            HasOptional(x => x.BundleLine)
                .WithMany()
                .HasForeignKey(x => x.BundleLineId);
        }
    }

    public class ProductOLProductMap : EntityTypeConfiguration<ProductOLProduct>
    {
        public ProductOLProductMap()
        {
            ToTable(nameof(ProductOLProduct));

            Property(x => x.Mark)
                .HasColumnType("VARCHAR")
                .HasColumnOrder(1)
                .HasMaxLength(126)
                .IsOptional();

            HasRequired(x => x.Product)
                .WithMany(x => x.OrderLines)
                .HasForeignKey(x => x.ProductId);
        }
    }

    public class ProductOLTimeMap : EntityTypeConfiguration<ProductOLTime>
    {
        public ProductOLTimeMap()
        {
            ToTable(nameof(ProductOLTime));

            HasRequired(x => x.ProductTime)
                .WithMany(x => x.OrderLines)
                .HasForeignKey(x => x.ProductTimeId);
        }
    }

    public class ProductOLTimeFixedMap : EntityTypeConfiguration<ProductOLTimeFixed>
    {
        public ProductOLTimeFixedMap()
        {
            ToTable(nameof(ProductOLTimeFixed));
        }
    }

    public class ProductOLSessionMap : EntityTypeConfiguration<ProductOLSession>
    {
        public ProductOLSessionMap()
        {
            ToTable(nameof(ProductOLSession));

            HasRequired(x => x.UsageSession)
                .WithMany()
                .HasForeignKey(x => x.UsageSessionId);
        }
    }
}
