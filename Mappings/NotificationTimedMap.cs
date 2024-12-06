using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Notification timed entity map.
    /// </summary>
    public sealed class NotificationTimedMap : IEntityTypeConfiguration<NotificationTimed>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<NotificationTimed> builder)
        {
            builder.ToTable(nameof(NotificationTimed))
                .HasBaseType<Notification>();

            builder.Property(notificationTimed => notificationTimed.Minute)
                .HasColumnOrder(0)
                .IsRequired();      
        }
    }
}
