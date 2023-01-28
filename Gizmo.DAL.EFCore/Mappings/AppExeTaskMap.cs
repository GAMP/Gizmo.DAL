using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class AppExeTaskMap : IEntityTypeConfiguration<AppExeTask>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppExeTask> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Indexes
            builder.HasIndex(x => x.AppExeId);

            // Table & Column Mappings
            builder.ToTable("AppExeTask");

            builder.Property(t => t.Id)
                .HasColumnName("AppExeTaskId");

            // Relationships
            builder.HasOne(x => x.TaskBase)
                .WithMany(x => x.UsedByAppExe)
                .HasForeignKey(x => x.TaskBaseId);

            builder.HasOne(t => t.AppExe)
                .WithMany(t => t.Tasks)
                .HasForeignKey(d => d.AppExeId);
        }
    }
}
