using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement branch filter entity map.
    /// </summary>
    public sealed class AchievementBranchFilterMap : IEntityTypeConfiguration<AchievementBranchFilter>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementBranchFilter> builder)
        {
            builder.ToTable(nameof(AchievementBranchFilter))
                .HasBaseType<AchievementFilter>();

            builder.Property(filter => filter.BranchId)
                .HasColumnOrder(1)
                .IsRequired();

            // restrict — removing the last ANY-of value would invert the filter meaning
            builder.HasOne(filter => filter.Branch)
                .WithMany()
                .HasForeignKey(filter => filter.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
