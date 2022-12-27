using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class ProductOrderMap : IEntityTypeConfiguration<ProductOrder>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("ProductOrderId")
                .HasColumnOrder(0);

            builder.Property(x => x.UserId)
                .HasColumnOrder(1);

            builder.Property(x => x.Status)
                .HasColumnOrder(2);

            builder.Property(x => x.SubTotal)
                .HasColumnOrder(3);

            builder.Property(x => x.Total)
                .HasColumnOrder(4);

            builder.Property(x => x.PointsTotal)
                .HasColumnOrder(5);

            builder.Property(x => x.Tax)
                .HasColumnOrder(6);

            builder.Property(x => x.HostId)
                .HasColumnOrder(7)
                .IsRequired(false);

            // Table & Column Mappings
            builder.ToTable(nameof(ProductOrder));

            builder.HasOne(x => x.User)
                .WithMany(x => x.ProductOrders)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Host)
                .WithMany(x => x.ProductOrders)
                .HasForeignKey(x => x.HostId);

            builder.HasOne(x => x.CreatedBy)
                .WithMany(x => x.CreatedOrders)
                .HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.ModifiedBy)
                .WithMany(x => x.ModifiedOrders)
                .HasForeignKey(x => x.ModifiedById);
        }
    }
}
