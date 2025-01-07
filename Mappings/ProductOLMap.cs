using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Product order line map.
    /// </summary>
    public class ProductOLBaseMap : IEntityTypeConfiguration<ProductOL>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ProductOL> builder)
        {
            builder.ToTable(nameof(ProductOL));

            builder.HasKey(productOrderLine => productOrderLine.Id);

            builder.Property(productOrderLine => productOrderLine.Id)
                .HasColumnOrder(0)
                .HasColumnName("ProductOLId");

            builder.Property(productOrderLine => productOrderLine.ProductOrderId)
                .HasColumnOrder(1);

            builder.Property(productOrderLine => productOrderLine.UserId)
                .HasColumnOrder(2);

            builder.Property(productOrderLine => productOrderLine.ProductName)
                .HasColumnOrder(3);

            builder.Property(productOrderLine => productOrderLine.Quantity)
                .HasColumnOrder(4);

            builder.Property(productOrderLine => productOrderLine.UnitPrice)
                .HasColumnOrder(5);

            builder.Property(productOrderLine => productOrderLine.UnitListPrice)
                .HasColumnOrder(6);

            builder.Property(productOrderLine => productOrderLine.UnitPointsPrice)
                .HasColumnOrder(7);

            builder.Property(productOrderLine => productOrderLine.UnitPointsListPrice)
                .HasColumnOrder(8);

            builder.Property(productOrderLine => productOrderLine.UnitCost)
                .HasColumnOrder(9);

            builder.Property(productOrderLine => productOrderLine.Cost)
                .HasColumnOrder(10);

            builder.Property(productOrderLine => productOrderLine.TaxRate)
                .HasColumnOrder(11);

            builder.Property(productOrderLine => productOrderLine.PreTaxTotal)
                .HasColumnOrder(12);

            builder.Property(productOrderLine => productOrderLine.Total)
                .HasColumnOrder(13);

            builder.Property(productOrderLine => productOrderLine.PointsTotal)
                .HasColumnOrder(14);

            builder.Property(productOrderLine => productOrderLine.Points)
                .HasColumnOrder(15);

            builder.Property(productOrderLine => productOrderLine.PointsAward)
                .HasColumnOrder(16);

            builder.Property(productOrderLine => productOrderLine.TaxTotal)
                .HasColumnOrder(17);

            builder.Property(productOrderLine => productOrderLine.PayType)
               .HasColumnOrder(18);

            builder.Property(productOrderLine => productOrderLine.IsDeleted)
                .HasColumnOrder(19);

            builder.Property(productOrderLine => productOrderLine.IsVoided)
                .HasColumnOrder(20);

            builder.Property(productOrderLine => productOrderLine.ReservationId)
                .IsRequired(false)
                .HasColumnOrder(21);

            builder.Property(productOrderLine => productOrderLine.ReservationHostId)
                .IsRequired(false)
                .HasColumnOrder(22);

            builder.Property(productOrderLine => productOrderLine.PrepareStatus)
                .IsRequired()
                .HasColumnOrder(23);

            builder.Property(productOrderLine => productOrderLine.PreparedQuantity)
                .IsRequired()
                .HasColumnOrder(24);

            builder.Property(productOrderLine => productOrderLine.PrepareTime)
                .IsRequired(false)
                .HasColumnOrder(25);

            builder.HasIndex(productOrderLine => productOrderLine.Id);

            builder.HasOne(productOrderLine => productOrderLine.ProductOrder)
                .WithMany(productOrderLine => productOrderLine.OrderLines)
                .HasForeignKey(x => x.ProductOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(productOrderLine => productOrderLine.User)
                .WithMany(productOrderLine => productOrderLine.ProductOrdersLines)
                .HasForeignKey(productOrderLine => productOrderLine.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(productOrderLine => productOrderLine.CreatedBy)
                .WithMany()
                .HasForeignKey(productOrderLine => productOrderLine.CreatedById);

            builder.HasOne(productOrderLine => productOrderLine.ModifiedBy)
                .WithMany()
                .HasForeignKey(productOrderLine => productOrderLine.ModifiedById);

            builder.HasOne(productOrderLine => productOrderLine.Reservation)
                .WithMany()
                .HasForeignKey(productOrderLine => productOrderLine.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(productOrderLine => productOrderLine.ReservationHost)
                .WithMany()
                .HasForeignKey(productOrderLine => productOrderLine.ReservationHostId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
