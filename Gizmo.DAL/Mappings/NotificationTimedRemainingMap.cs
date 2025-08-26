using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Notification timed remaining entity configuration.
    /// </summary>
    public sealed class NotificationTimedRemainingMap : IEntityTypeConfiguration<NotificationTimedRemaining>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<NotificationTimedRemaining> builder)
        {
            builder.ToTable(nameof(NotificationTimedRemaining))
                .HasBaseType<NotificationTimed>();
        }
    }
}
