using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement app executable filter entity map.
    /// </summary>
    public sealed class AchievementAppExeFilterMap : IEntityTypeConfiguration<AchievementAppExeFilter>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementAppExeFilter> builder)
        {
            builder.ToTable(nameof(AchievementAppExeFilter))
                .HasBaseType<AchievementFilter>();

            builder.Property(filter => filter.AppExeId)
                .HasColumnOrder(1)
                .IsRequired();

            // restrict — removing the last ANY-of value would invert the filter meaning
            builder.HasOne(filter => filter.AppExe)
                .WithMany()
                .HasForeignKey(filter => filter.AppExeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
