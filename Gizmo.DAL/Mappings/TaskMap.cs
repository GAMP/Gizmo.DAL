using Gizmo.DAL.Entities;

using GizmoDALV2;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class TaskBaseMap : EntityTypeConfiguration<TaskBase>
    {
        public TaskBaseMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(t => t.Name)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            Property(x => x.Guid)
                .IsRequired()
                .HasColumnOrder(2)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true }
                }));

            // Table & Column Mappings
            ToTable("TaskBase");

            Property(t => t.Id)
                .HasColumnName("TaskId");
        }
    }

    public class TaskProcessMap : EntityTypeConfiguration<TaskProcess>
    {
        public TaskProcessMap()
        {
            // Properties
            Property(t => t.FileName)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.Arguments)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.WorkingDirectory)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.Username)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.Password)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.TINY45);

            Property(x => x.ProcessOptions)
                .HasColumnOrder(6);

            // Table & Column Mappings
            ToTable("TaskProcess");

        }
    }

    public class TaskJunctionMap : EntityTypeConfiguration<TaskJunction>
    {
        public TaskJunctionMap()
        {
            // Properties
            Property(x => x.SourceDirectory)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired();

            Property(x => x.DestinationDirectory)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired();

            // Table & Column Mappings
            ToTable("TaskJunction");
        }
    }

    public class TaskNotificationMap : EntityTypeConfiguration<TaskNotification>
    {
        public TaskNotificationMap()
        {
            // Properties
            Property(x => x.Title)
                .HasMaxLength(SQLStringSize.TINY);

            Property(x => x.Message)
                .HasMaxLength(SQLStringSize.NORMAL);

            Property(x => x.NotificationOptions);

            // Table & Column Mappings
            ToTable("TaskNotification");
        }
    }

    public class TaskScriptMap : EntityTypeConfiguration<TaskScript>
    {
        public TaskScriptMap()
        {
            // Properties
            Property(x => x.Data)
                .HasMaxLength(SQLStringSize.NORMAL);

            // Table & Column Mappings
            ToTable("TaskScript");

        }
    }

    public class ClientTaskMap : EntityTypeConfiguration<ClientTask>
    {
        public ClientTaskMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("ClientTaskId");

            // Table & Column Mappings
            HasRequired(x => x.TaskBase)
                .WithMany(x => x.UsedByTask)
                .HasForeignKey(x => x.TaskBaseId);

            ToTable("ClientTask");
        }
    }
}
