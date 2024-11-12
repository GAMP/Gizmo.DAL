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
                .HasColumnName("PromotionId");

            builder.Property(x => x.Name)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Description)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired(false);

            builder.Property(x => x.Template)
                .IsRequired(false);
        }
    }
}
