using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Discount basic entity map.
    /// </summary>
    public sealed class DiscountBasicMap : IEntityTypeConfiguration<DiscountBasic>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DiscountBasic> builder)
        {
            builder.ToTable(nameof(DiscountBasic))
                .HasBaseType<DiscountTargeted>();

            builder.Property(discountBasic => discountBasic.Id)
                .HasColumnOrder(0);

            builder.Property(discountBasic => discountBasic.ApplyType)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(discountBasic => discountBasic.Type)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(discountBasic => discountBasic.Value)
                .IsRequired(false)
                .HasColumnOrder(3);
        }
    }
}
