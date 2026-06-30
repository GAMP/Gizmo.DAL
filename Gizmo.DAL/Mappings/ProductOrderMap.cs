using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Product entity map.
    /// </summary>
    public class ProductOrderMap : IEntityTypeConfiguration<ProductOrder>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.ToTable(nameof(ProductOrder));

            builder.HasKey(productOrder => productOrder.Id);

            builder.Property(productOrder => productOrder.Id)
                .HasColumnName("ProductOrderId")
                .HasColumnOrder(0);

            builder.Property(productOrder => productOrder.UserId)
                .HasColumnOrder(1);

            builder.Property(productOrder => productOrder.Status)
                .HasColumnOrder(2);

            builder.Property(productOrder => productOrder.SubTotal)
                .HasColumnOrder(3);

            builder.Property(productOrder => productOrder.Total)
                .HasColumnOrder(4);

            builder.Property(productOrder => productOrder.PointsTotal)
                .HasColumnOrder(5);

            builder.Property(productOrder => productOrder.Tax)
                .HasColumnOrder(6);

            builder.Property(productOrder => productOrder.HostId)
                .HasColumnOrder(7)
                .IsRequired(false);

            builder.Property(productOrder => productOrder.PrepareStatus)
                .IsRequired();

            builder.Property(productOrder => productOrder.PreparedQuantity)
                .IsRequired();

            builder.Property(productOrder => productOrder.PrepareTime)
                .IsRequired(false);

            builder.Property(productOrder => productOrder.BranchId)
                .IsRequired(false);

            builder.HasOne(productOrder => productOrder.User)
                .WithMany(productOrder => productOrder.ProductOrders)
                .HasForeignKey(productOrder => productOrder.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(productOrder => productOrder.Host)
                .WithMany(productOrder => productOrder.ProductOrders)
                .HasForeignKey(productOrder => productOrder.HostId);

            builder.HasOne(productOrder => productOrder.CreatedBy)
                .WithMany(productOrder => productOrder.CreatedOrders)
                .HasForeignKey(productOrder => productOrder.CreatedById);

            builder.HasOne(productOrder => productOrder.ModifiedBy)
                .WithMany(productOrder => productOrder.ModifiedOrders)
                .HasForeignKey(productOrder => productOrder.ModifiedById);

            builder.HasOne(productOrder => productOrder.Branch)
                .WithMany(branch => branch.Orders)
                .HasForeignKey(productOrder => productOrder.BranchId)
                .OnDelete(DeleteBehavior.NoAction);

            // Orders admin list (ProductOrderService.GetAsync) filters the ~867K-row table by a CreatedTime
            // range + Status and otherwise full-scans it (~26,500 logical reads). Status is sargable here
            // (the LINQ predicate is Status == OrderStatus.X; the HasFlag is on the client-side filter flag in
            // C#, not the SQL). CreatedTime is a shadow property → specified by name. (The filtered active-queue
            // index — Status IN (OnHold,Accepted,Processing) — lives in ApplyPerformanceIndexes.)
            builder.HasIndex("CreatedTime", nameof(ProductOrder.Status));
        }
    }
}
