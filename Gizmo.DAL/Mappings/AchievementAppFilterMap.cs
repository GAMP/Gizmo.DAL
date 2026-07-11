using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement app filter entity map.
    /// </summary>
    public sealed class AchievementAppFilterMap : IEntityTypeConfiguration<AchievementAppFilter>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementAppFilter> builder)
        {
            builder.ToTable(nameof(AchievementAppFilter))
                .HasBaseType<AchievementFilter>();

            builder.Property(filter => filter.AppId)
                .HasColumnOrder(1)
                .IsRequired();

            // restrict — removing the last ANY-of value would invert the filter meaning
            builder.HasOne(filter => filter.App)
                .WithMany()
                .HasForeignKey(filter => filter.AppId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
