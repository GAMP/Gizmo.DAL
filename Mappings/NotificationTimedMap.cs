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

            builder.Property(notificationTimed => notificationTimed.Type)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(notificationTimed => notificationTimed.FocusType)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(notificationTimed => notificationTimed.Message)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired(false);
        }
    }
}
