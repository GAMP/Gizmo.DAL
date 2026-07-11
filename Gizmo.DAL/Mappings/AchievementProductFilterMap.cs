using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement product filter entity map.
    /// </summary>
    public sealed class AchievementProductFilterMap : IEntityTypeConfiguration<AchievementProductFilter>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementProductFilter> builder)
        {
            builder.ToTable(nameof(AchievementProductFilter))
                .HasBaseType<AchievementFilter>();

            builder.Property(filter => filter.ProductId)
                .HasColumnOrder(1)
                .IsRequired();

            // restrict — removing the last ANY-of value would invert the filter meaning
            builder.HasOne(filter => filter.Product)
                .WithMany()
                .HasForeignKey(filter => filter.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
