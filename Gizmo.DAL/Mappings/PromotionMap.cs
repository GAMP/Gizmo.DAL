using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Promotion entity map.
    /// </summary>
    public sealed class PromotionMap : IEntityTypeConfiguration<Promotion>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable(nameof(Promotion))
                .UseTptMappingStrategy();

            builder.HasKey(promotion => promotion.Id);

            builder.Property(promotion => promotion.Id)
                .HasColumnOrder(0)
                .HasColumnName("PromotionId");

            builder.Property(promotion => promotion.Name)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(promotion => promotion.Description)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired(false);

            builder.Property(promotion => promotion.CodeType)
                .HasColumnOrder(3)
                .IsRequired();
        }
    }
}
