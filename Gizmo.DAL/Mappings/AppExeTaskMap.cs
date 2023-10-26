using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AppExeTaskMap : EntityTypeConfiguration<AppExeTask>
    {
        public AppExeTaskMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Table & Column Mappings
            ToTable("AppExeTask");

            Property(t => t.Id)
                .HasColumnName("AppExeTaskId");

            // Relationships
            HasRequired(x => x.TaskBase)
                .WithMany(x => x.UsedByAppExe)
                .HasForeignKey(x => x.TaskBaseId);

            HasRequired(t => t.AppExe)
                .WithMany(t => t.Tasks)
                .HasForeignKey(d => d.AppExeId);
        }
    }
}
