using GizmoDALV2;
using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class TaskNotificationMap : IEntityTypeConfiguration<TaskNotification>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<TaskNotification> builder)
        {
            // Properties
            builder.Property(x => x.Title)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.Message)
                .HasMaxLength(SQLStringSize.NORMAL);

            builder.Property(x => x.NotificationOptions);

            // Indexes
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("TaskNotification");
        }
    }
}
