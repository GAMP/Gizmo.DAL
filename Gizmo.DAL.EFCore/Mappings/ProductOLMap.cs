using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class ProductOLBaseMap : IEntityTypeConfiguration<ProductOL>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductOL> builder)
        {
            builder.ToTable(nameof(ProductOL));

            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("ProductOLId");

            builder.Property(x => x.ProductOrderId)
                .HasColumnOrder(1);

            builder.Property(x => x.UserId)
                .HasColumnOrder(2);

            builder.Property(x => x.ProductName)
                .HasColumnOrder(3);

            builder.Property(x => x.Quantity)
                .HasColumnOrder(4);

            builder.Property(x => x.UnitPrice)
                .HasColumnOrder(5);

            builder.Property(x => x.UnitListPrice)
                .HasColumnOrder(6);

            builder.Property(x => x.UnitPointsPrice)
                .HasColumnOrder(7);

            builder.Property(x => x.UnitPointsListPrice)
                .HasColumnOrder(8);

            builder.Property(x => x.UnitCost)
                .HasColumnOrder(9);

            builder.Property(x => x.Cost)
                .HasColumnOrder(10);

            builder.Property(x => x.TaxRate)
                .HasColumnOrder(11);

            builder.Property(x => x.PreTaxTotal)
                .HasColumnOrder(12);

            builder.Property(x => x.Total)
                .HasColumnOrder(13);

            builder.Property(x => x.PointsTotal)
                .HasColumnOrder(14);

            builder.Property(x => x.Points)
                .HasColumnOrder(15);

            builder.Property(x => x.PointsAward)
                .HasColumnOrder(16);

            builder.Property(x => x.TaxTotal)
                .HasColumnOrder(17);

            builder.Property(x => x.PayType)
               .HasColumnOrder(18);

            builder.Property(x => x.IsDeleted)
                .HasColumnOrder(19);

            builder.Property(x => x.IsVoided)
                .HasColumnOrder(20);

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.ProductOrder)
                .WithMany(x => x.OrderLines)
                .HasForeignKey(x => x.ProductOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(x => x.ProductOrdersLines)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.ModifiedBy)
                .WithMany()
                .HasForeignKey(x => x.ModifiedById);
        }
    }
}
