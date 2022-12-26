using Gizmo.DAL.Entities;
using GizmoDALV2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class DeviceHdmiMap : IEntityTypeConfiguration<DeviceHdmi>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<DeviceHdmi> builder)
        {
            //serial number column configuration
            builder.Property(x => x.UniqueId)
                .HasColumnOrder(3)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            // Indexes
            builder.HasIndex(t => t.UniqueId).HasDatabaseName("UQ_UniqueId").IsUnique().HasFilter(null);
            builder.HasIndex(x => x.Id);

            //table configuration
            builder.ToTable(nameof(DeviceHdmi));
        }
    }
}
