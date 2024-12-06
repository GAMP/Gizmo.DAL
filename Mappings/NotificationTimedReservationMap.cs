using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Notification timed reservation map.
    /// </summary>
    public sealed class NotificationTimedReservationMap : IEntityTypeConfiguration<NotificationTimedReservation>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<NotificationTimedReservation> builder)
        {
            builder.ToTable(nameof(NotificationTimedReservation))
                .HasBaseType<NotificationTimed>();
        }
    }
}
