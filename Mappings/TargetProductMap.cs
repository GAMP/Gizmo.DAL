using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Target group target product entity map.
    /// </summary>
    public sealed class TargetProductMap : IEntityTypeConfiguration<TargetProduct>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetProduct> builder)
        {
            builder.ToTable(nameof(TargetProduct))
                .HasBaseType<Target>();

            builder.Property(targetProduct => targetProduct.Id)
                .IsRequired()
                .HasColumnOrder(0);

            builder.Property(targetProduct => targetProduct.TargetGroupProductId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(targetProduct => targetProduct.ProductId)
                .IsRequired()
                .HasColumnOrder(2);

            builder.HasIndex(target => new { target.TargetGroupProductId, target.ProductId })
                .IsUnique()
                .HasFilter(null);

            builder.HasOne(target => target.TargetGroupProduct)
                .WithMany(targetGroup => targetGroup.Products)
                .HasForeignKey(target => target.TargetGroupProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(target => target.Product)
                .WithMany()
                .HasForeignKey(target => target.ProductId)
                .OnDelete(DeleteBehavior.Cascade);            
        }
    }
}
