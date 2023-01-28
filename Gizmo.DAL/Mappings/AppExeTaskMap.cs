using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class AppExeTaskMap : EntityTypeConfiguration<AppExeTask>
    {
        public AppExeTaskMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("AppExeTask");

            this.Property(t => t.Id)
                .HasColumnName("AppExeTaskId");

            // Relationships
            this.HasRequired(x => x.TaskBase)
                .WithMany(x => x.UsedByAppExe)
                .HasForeignKey(x => x.TaskBaseId);

            this.HasRequired(t => t.AppExe)
                .WithMany(t => t.Tasks)
                .HasForeignKey(d => d.AppExeId);
        }
    }
}
