using GizmoDALV2;
using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
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
}
