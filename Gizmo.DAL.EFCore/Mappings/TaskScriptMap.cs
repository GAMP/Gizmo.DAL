using GizmoDALV2;
using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
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
}
