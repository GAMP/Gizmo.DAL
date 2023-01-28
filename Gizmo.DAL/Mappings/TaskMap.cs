using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace GizmoDALV2.Mappings
{
    public class TaskBaseMap : EntityTypeConfiguration<TaskBase>
    {
        public TaskBaseMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(t => t.Name)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true } 
                }));

            this.Property(x => x.Guid)
                .IsRequired()
                .HasColumnOrder(2)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true } 
                }));

            // Table & Column Mappings
            this.ToTable("TaskBase");

            this.Property(t => t.Id)
                .HasColumnName("TaskId");     
        }
    }

    public class TaskProcessMap : EntityTypeConfiguration<TaskProcess>
    {
        public TaskProcessMap()
        {
            // Properties
            this.Property(t => t.FileName)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.Arguments)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.WorkingDirectory)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.Username)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.Password)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.TINY45);

            this.Property(x => x.ProcessOptions)
                .HasColumnOrder(6);

            // Table & Column Mappings
            this.ToTable("TaskProcess");

        }
    }

    public class TaskJunctionMap : EntityTypeConfiguration<TaskJunction>
    {
        public TaskJunctionMap()
        {
            // Properties
            this.Property(x => x.SourceDirectory)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired();

            this.Property(x => x.DestinationDirectory)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("TaskJunction");
        }
    }

    public class TaskNotificationMap : EntityTypeConfiguration<TaskNotification>
    {
        public TaskNotificationMap()
        {
            // Properties
            this.Property(x => x.Title)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(x => x.Message)
                .HasMaxLength(SQLStringSize.NORMAL);

            this.Property(x => x.NotificationOptions);

            // Table & Column Mappings
            this.ToTable("TaskNotification");
        }
    }

    public class TaskScriptMap : EntityTypeConfiguration<TaskScript>
    {
        public TaskScriptMap()
        {
            // Properties
            this.Property(x => x.Data)
                .HasMaxLength(SQLStringSize.NORMAL);

            // Table & Column Mappings
            this.ToTable("TaskScript");

        }
    }

    public class ClientTaskMap : EntityTypeConfiguration<ClientTask>
    {
        public ClientTaskMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("ClientTaskId");

            // Table & Column Mappings
            this.HasRequired(x => x.TaskBase)
                .WithMany(x=>x.UsedByTask)
                .HasForeignKey(x => x.TaskBaseId);

            this.ToTable("ClientTask");
        }
    }
}
