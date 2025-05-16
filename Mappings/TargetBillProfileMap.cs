using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Target group target billing profile entity map.
    /// </summary>
    public sealed class TargetBillProfileMap : IEntityTypeConfiguration<TargetBillProfile>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetBillProfile> builder)
        {
            builder.ToTable(nameof(TargetBillProfile))
                .HasBaseType<Target>();

            builder.Property(targetBillProfile => targetBillProfile.Id)
                .IsRequired()
                .HasColumnOrder(0);

            builder.Property(targetBillProfile => targetBillProfile.TargetGroupBillProfileId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(targetBillProfile => targetBillProfile.BillProfileId)
                .IsRequired()
                .HasColumnOrder(2);

            builder.HasOne(target => target.BillProfile)
                .WithMany()
                .HasForeignKey(target => target.BillProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(target => target.TargetGroupBillProfile)
                .WithMany(targetGroup => targetGroup.BillProfiles)
                .HasForeignKey(target => target.TargetGroupBillProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(target => new { target.TargetGroupBillProfileId, target.BillProfileId })
                .IsUnique()
                .HasFilter(null);
        }
    }
}
