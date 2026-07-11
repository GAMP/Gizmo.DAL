using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement product group filter entity map.
    /// </summary>
    public sealed class AchievementProductGroupFilterMap : IEntityTypeConfiguration<AchievementProductGroupFilter>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementProductGroupFilter> builder)
        {
            builder.ToTable(nameof(AchievementProductGroupFilter))
                .HasBaseType<AchievementFilter>();

            builder.Property(filter => filter.ProductGroupId)
                .HasColumnOrder(1)
                .IsRequired();

            // restrict — removing the last ANY-of value would invert the filter meaning
            builder.HasOne(filter => filter.ProductGroup)
                .WithMany()
                .HasForeignKey(filter => filter.ProductGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
