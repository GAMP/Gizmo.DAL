using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class DeviceHostMap : IEntityTypeConfiguration<DeviceHost>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<DeviceHost> builder)
        {
            //primary key configuration
            builder.HasKey(e => e.Id);

            //primary key column configuration
            builder.Property(e => e.Id)
                .HasColumnOrder(0)
                .HasColumnName("DeviceHostId");

            //device id column configuration
            builder.Property(e => e.DeviceId)
                .HasColumnName(nameof(DeviceHost.DeviceId))
                .HasColumnOrder(1)
                .IsRequired();

            //host id column configuration
            builder.Property(e => e.HostId)
                .HasColumnName(nameof(DeviceHost.HostId))
                .HasColumnOrder(2)
                .IsRequired();

            //host navigational mappings
            builder.HasOne(e => e.Host)
                .WithMany(e => e.Devices)
                .HasForeignKey(e => e.HostId);

            //device navigational mappings
            builder.HasOne(e => e.Device)
                .WithMany(e => e.Hosts)
                .HasForeignKey(e => e.DeviceId);

            // Indexes 
            builder.HasIndex(t => new { t.DeviceId, t.HostId }, "UQ_HostDevice").IsUnique();

            //table configuration
            builder.ToTable(nameof(DeviceHost));
        }
    }
}
