using GizmoDALV2;
using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class TaskBaseMap : IEntityTypeConfiguration<TaskBase>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<TaskBase> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(t => t.Name)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Guid)
                .IsRequired()
                .HasColumnOrder(2);

            // Indexes
            builder.HasIndex(t => t.Guid).HasDatabaseName("UQ_Guid").IsUnique();
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("TaskBase");

            builder.Property(t => t.Id)
                .HasColumnName("TaskId");     
        }
    }

    public class TaskProcessMap : IEntityTypeConfiguration<TaskProcess>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<TaskProcess> builder)
        {
            // Properties
            builder.Property(t => t.FileName)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.Arguments)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.WorkingDirectory)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.Username)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.Password)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.ProcessOptions)
                .HasColumnOrder(6);

            // Indexes
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("TaskProcess");

        }
    }

    public class TaskJunctionMap : IEntityTypeConfiguration<TaskJunction>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<TaskJunction> builder)
        {
            // Properties
            builder.Property(x => x.SourceDirectory)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired();

            builder.Property(x => x.DestinationDirectory)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired();

            // Indexes
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("TaskJunction");
        }
    }

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

    public class TaskScriptMap : IEntityTypeConfiguration<TaskScript>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<TaskScript> builder)
        {
            // Properties
            builder.Property(x => x.Data)
                .HasMaxLength(SQLStringSize.NORMAL);

            // Indexes
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("TaskScript");

        }
    }

    public class ClientTaskMap : IEntityTypeConfiguration<ClientTask>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ClientTask> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("ClientTaskId");

            // Table & Column Mappings
            builder.HasOne(x => x.TaskBase)
                .WithMany(x=>x.UsedByTask)
                .HasForeignKey(x => x.TaskBaseId);

            builder.ToTable("ClientTask");
        }
    }
}
