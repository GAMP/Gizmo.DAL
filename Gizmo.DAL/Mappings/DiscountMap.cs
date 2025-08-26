using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Discount entity map.
    /// </summary>
    public sealed class DiscountMap : IEntityTypeConfiguration<Discount>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable(nameof(Discount));

            builder.HasKey(discount => discount.Id);

            builder.Property(discount => discount.Id)
                .HasColumnOrder(0)
                .HasColumnName("DiscountId");

            builder.Property(discount => discount.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(discount => discount.Description)
                .IsRequired(false)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(discountBase => discountBase.ApplyType)
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(discountBase => discountBase.CalculationType)
                .IsRequired()
                .HasColumnOrder(4);

            builder.Property(discountBase => discountBase.RewardType)
                .IsRequired()
                .HasColumnOrder(5);

            builder.Property(discountBase => discountBase.Requirement)
                .IsRequired()
                .HasColumnOrder(6);

            builder.Property(discountBase => discountBase.Value)
                .IsRequired()
                .HasColumnOrder(7);

            builder.Property(discountBase => discountBase.IsDisabled)
                .IsRequired()
                .HasColumnOrder(8);

            builder.Property(discountBase => discountBase.IsDeleted)
                .IsRequired()
                .HasColumnOrder(9);

            builder.HasIndex(discountBase => discountBase.Name)
                .IsUnique();
        }
    }
}
