using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement app group filter entity map.
    /// </summary>
    public sealed class AchievementAppGroupFilterMap : IEntityTypeConfiguration<AchievementAppGroupFilter>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementAppGroupFilter> builder)
        {
            builder.ToTable(nameof(AchievementAppGroupFilter))
                .HasBaseType<AchievementFilter>();

            builder.Property(filter => filter.AppGroupId)
                .HasColumnOrder(1)
                .IsRequired();

            // restrict — removing the last ANY-of value would invert the filter meaning
            builder.HasOne(filter => filter.AppGroup)
                .WithMany()
                .HasForeignKey(filter => filter.AppGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
