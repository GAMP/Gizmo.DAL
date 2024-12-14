using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Discount group entity map.
    /// </summary>
    public sealed class DiscountGroupMap : IEntityTypeConfiguration<DiscountGroup>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DiscountGroup> builder)
        {
            builder.HasKey(discountGroup => discountGroup.Id);

            builder.Property(discountGroup => discountGroup.Id)
                .HasColumnName("DiscountGroupId")
                .HasColumnOrder(0);

            builder.Property(discountGroup => discountGroup.Name)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired();

            builder.HasIndex(discountGroup => discountGroup.Name)
                .IsUnique();
        }
    }
}
