using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Notification entity configuration.
    /// </summary>
    public sealed class NotificationMap : IEntityTypeConfiguration<Notification>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable(nameof(Notification))
                .UseTptMappingStrategy();

            builder.HasKey(notification => notification.Id);

            builder.Property(notification => notification.Id)
                .HasColumnName("NotificationId")
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(notification => notification.IsDisabled)
                .HasColumnOrder(1)
                .IsRequired();
        }
    }
}
