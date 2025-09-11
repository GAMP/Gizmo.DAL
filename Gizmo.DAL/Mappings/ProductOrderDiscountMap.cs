using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Product order discount map.
    /// </summary>
    public sealed class ProductOrderDiscountMap : IEntityTypeConfiguration<ProductOrderDiscount>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ProductOrderDiscount> builder)
        {
            builder.ToTable(nameof(ProductOrderDiscount));

            builder.HasKey(productOrderDiscount => productOrderDiscount.Id);

            builder.Property(productOrderDiscount => productOrderDiscount.Id)
                .HasColumnName("ProductOrderDiscountId")
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(productOrderDiscount => productOrderDiscount.UserId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(productOrderDiscount => productOrderDiscount.ProductOrderId)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(productOrderDiscount => productOrderDiscount.ProductOrderLineId)
                .HasColumnOrder(3)
                .IsRequired(false);

            builder.Property(productOrderDiscount => productOrderDiscount.PromotionId)
                .HasColumnOrder(4)
                .IsRequired(false);

            builder.Property(productOrderDiscount => productOrderDiscount.PromotionCodeId)
                .HasColumnOrder(5)
                .IsRequired(false);

            builder.Property(productOrderDiscount => productOrderDiscount.DiscountId)
                .HasColumnOrder(6)
                .IsRequired();

            builder.Property(productOrderDiscount => productOrderDiscount.DiscountName)
                .HasColumnOrder(7)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired();

            builder.Property(productOrderDiscount => productOrderDiscount.CalculationType)
                .HasColumnOrder(8)
                .IsRequired();

            builder.Property(productOrderDiscount => productOrderDiscount.ApplyType)
                .HasColumnOrder(9)
                .IsRequired();

            builder.Property(productOrderDiscount => productOrderDiscount.Value)
                .HasColumnOrder(10)
                .IsRequired();

            builder.Property(productOrderDiscount => productOrderDiscount.Discount)
                .HasColumnOrder(11)
                .IsRequired();

            builder.HasOne(productOrderDiscount => productOrderDiscount.User)
                .WithMany()
                .HasForeignKey(productOrderDiscount => productOrderDiscount.UserId);

            builder.HasOne(productOrderDiscount => productOrderDiscount.ProductOrder)
                .WithMany(productOrder => productOrder.Discounts)
                .HasForeignKey(productOrderDiscount => productOrderDiscount.ProductOrderId);

            builder.HasOne(productOrderDiscount => productOrderDiscount.ProductOrderLine)
                .WithMany(productOrderLine => productOrderLine.Discounts)
                .HasForeignKey(productOrderDiscount => productOrderDiscount.ProductOrderLineId);

            builder.HasOne(productOrderDiscount => productOrderDiscount.Promotion)
                .WithMany()
                .HasForeignKey(productOrderDiscount => productOrderDiscount.PromotionId);

            builder.HasOne(productOrderDiscount => productOrderDiscount.PromotionCode)
                .WithMany()
                .HasForeignKey(productOrderDiscount => productOrderDiscount.PromotionCodeId);

            builder.HasOne(productOrderDiscount => productOrderDiscount.OrderDiscount)
                .WithMany()
                .HasForeignKey(productOrderDiscount => productOrderDiscount.DiscountId);
        }
    }
}
