using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement bill profile filter entity map.
    /// </summary>
    public sealed class AchievementBillProfileFilterMap : IEntityTypeConfiguration<AchievementBillProfileFilter>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementBillProfileFilter> builder)
        {
            builder.ToTable(nameof(AchievementBillProfileFilter))
                .HasBaseType<AchievementFilter>();

            builder.Property(filter => filter.BillProfileId)
                .HasColumnOrder(1)
                .IsRequired();

            // restrict — removing the last ANY-of value would invert the filter meaning
            builder.HasOne(filter => filter.BillProfile)
                .WithMany()
                .HasForeignKey(filter => filter.BillProfileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
