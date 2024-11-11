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
        }
    }
}
