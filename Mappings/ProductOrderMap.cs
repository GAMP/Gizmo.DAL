using Gizmo.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ProductOrderMap : EntityTypeConfiguration<ProductOrder>
    {
        public ProductOrderMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("ProductOrderId")
                .HasColumnOrder(0);

            Property(x => x.UserId)
                .HasColumnOrder(1);

            Property(x => x.Status)
                .HasColumnOrder(2);

            Property(x => x.SubTotal)
                .HasColumnOrder(3);

            Property(x => x.Total)
                .HasColumnOrder(4);

            Property(x => x.PointsTotal)
                .HasColumnOrder(5);

            Property(x => x.Tax)
                .HasColumnOrder(6);

            Property(x => x.HostId)
                .HasColumnOrder(7)
                .IsOptional();

            // Table & Column Mappings
            ToTable(nameof(ProductOrder));

            HasOptional(x => x.User)
                .WithMany(x => x.ProductOrders)
                .HasForeignKey(x => x.UserId);

            HasOptional(x => x.Host)
                .WithMany(x => x.ProductOrders)
                .HasForeignKey(x => x.HostId);

            HasOptional(x => x.CreatedBy)
                .WithMany(x => x.CreatedOrders)
                .HasForeignKey(x => x.CreatedById);

            HasOptional(x => x.ModifiedBy)
                .WithMany(x => x.ModifiedOrders)
                .HasForeignKey(x => x.ModifiedById);
        }
    }
}
