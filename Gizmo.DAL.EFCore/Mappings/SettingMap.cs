using GizmoDALV2;
using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Settings entity map.
    /// </summary>
    public class SettingMap : IEntityTypeConfiguration<Setting>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(t => t.GroupName)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.HasIndex(t => new { t.Name, t.GroupName }).HasDatabaseName("UQ_NameGroup").IsUnique().HasFilter(null);

            builder.Property(t => t.Value)
                .HasColumnOrder(3);

            // Table & Column Mappings
            builder.ToTable("Setting");

            builder.Property(x => x.Id)
                .HasColumnName("SettingId");
        }
    }
}
