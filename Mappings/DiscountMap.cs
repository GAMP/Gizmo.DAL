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
            builder.ToTable(nameof(Discount))
                   .UseTptMappingStrategy();

            builder.HasKey(discountBase => discountBase.Id);

            builder.Property(discountBase => discountBase.Id)
                .HasColumnOrder(0)
                .HasColumnName("DiscountId");

            builder.Property(discountBase => discountBase.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(discountBase => discountBase.Description)
                .IsRequired(false)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(discountBase => discountBase.IsDeleted)
                .IsRequired()
                .HasColumnOrder(3);

            builder.HasIndex(discountBase => discountBase.Name)
                .IsUnique();
        }
    }
}
