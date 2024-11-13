using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Promotion branch entity map.
    /// </summary>
    public class PromotionBranchMap : IEntityTypeConfiguration<PromotionBranch>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<PromotionBranch> builder)
        {
            builder.ToTable(nameof(PromotionBranch));

            builder.HasKey(t => new { t.PromotionId, t.BranchId });

            builder.Property(e => e.PromotionId)
                .HasColumnOrder(0);
            builder.Property(e => e.BranchId)
                .HasColumnOrder(1);
            builder.Property(e => e.IsEnabled)
                .HasColumnOrder(2);

            builder.HasIndex(t => new { t.PromotionId, t.BranchId }).IsUnique().HasFilter(null);

            builder.HasOne(x => x.Branch)
                .WithMany(x => x.Promotions)
                .HasForeignKey(x => x.BranchId);          
        }
    }
}
