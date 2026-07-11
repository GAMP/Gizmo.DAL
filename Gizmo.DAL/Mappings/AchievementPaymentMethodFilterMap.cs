using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement payment method filter entity map.
    /// </summary>
    public sealed class AchievementPaymentMethodFilterMap : IEntityTypeConfiguration<AchievementPaymentMethodFilter>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementPaymentMethodFilter> builder)
        {
            builder.ToTable(nameof(AchievementPaymentMethodFilter))
                .HasBaseType<AchievementFilter>();

            builder.Property(filter => filter.PaymentMethodId)
                .HasColumnOrder(1)
                .IsRequired();

            // restrict — removing the last ANY-of value would invert the filter meaning
            builder.HasOne(filter => filter.PaymentMethod)
                .WithMany()
                .HasForeignKey(filter => filter.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
