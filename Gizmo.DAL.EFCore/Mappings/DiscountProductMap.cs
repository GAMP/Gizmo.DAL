using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class DiscountProductMap : IEntityTypeConfiguration<DiscountProduct>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<DiscountProduct> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnOrder(0)
                .HasColumnName("DiscountProductId");

            // Table & Column Mappings
            builder.ToTable("DiscountUser");

            // Relationships

            builder.HasOne(x => x.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Discount)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
