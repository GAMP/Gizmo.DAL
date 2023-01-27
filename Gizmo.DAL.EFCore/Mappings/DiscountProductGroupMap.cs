using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class DiscountProductGroupMap : IEntityTypeConfiguration<DiscountProductGroup>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<DiscountProductGroup> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnOrder(0)
                .HasColumnName("DiscountProductGroupId");

            // Table & Column Mappings
            builder.ToTable("DiscountUser");

            // Relationships

            builder.HasOne(x => x.ProductGroup)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Discount)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
