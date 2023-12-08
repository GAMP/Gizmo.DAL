using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class DeviceMap : IEntityTypeConfiguration<Device>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            //primary key configuration
            builder.HasKey(e => e.Id);

            //primary key column configuration
            builder.Property(e => e.Id)
                .HasColumnOrder(0)
                .HasColumnName("DeviceId");

            //device name configuration
            builder.Property(e => e.Name)
                .HasColumnOrder(1)
                .HasColumnName(nameof(Device.Name))
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY45);

            // Indexes
            builder.HasIndex(t => t.Name).IsUnique();

            //is enabled property
            builder.Property(e => e.IsEnabled)
                .HasColumnOrder(2);

            //table name configuration
            builder.ToTable(nameof(Device));
        }
    }
}
