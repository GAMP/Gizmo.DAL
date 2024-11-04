using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class FeedBranchMap : IEntityTypeConfiguration<FeedBranch>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<FeedBranch> builder)
        {
            builder.HasKey(t => new { t.FeedId, t.BranchId });

            builder.Property(e => e.FeedId)
                .HasColumnOrder(0);
            builder.Property(e => e.BranchId)
                .HasColumnOrder(1);
            builder.Property(e => e.IsEnabled)
                .HasColumnOrder(3);

            builder.HasIndex(t => new { t.FeedId, t.BranchId }).IsUnique().HasFilter(null);

            builder.HasOne(x => x.Branch)
                .WithMany(x => x.Feeds)
                .HasForeignKey(x => x.BranchId);

            builder.ToTable(nameof(FeedBranch));
        }
    }
}
