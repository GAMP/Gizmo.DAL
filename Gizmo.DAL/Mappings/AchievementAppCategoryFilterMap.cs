using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement app category filter entity map.
    /// </summary>
    public sealed class AchievementAppCategoryFilterMap : IEntityTypeConfiguration<AchievementAppCategoryFilter>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementAppCategoryFilter> builder)
        {
            builder.ToTable(nameof(AchievementAppCategoryFilter))
                .HasBaseType<AchievementFilter>();

            builder.Property(filter => filter.AppCategoryId)
                .HasColumnOrder(1)
                .IsRequired();

            // restrict — removing the last ANY-of value would invert the filter meaning
            builder.HasOne(filter => filter.AppCategory)
                .WithMany()
                .HasForeignKey(filter => filter.AppCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
