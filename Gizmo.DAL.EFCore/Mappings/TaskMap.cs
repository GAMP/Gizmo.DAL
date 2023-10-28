using GizmoDALV2;
using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
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
}
