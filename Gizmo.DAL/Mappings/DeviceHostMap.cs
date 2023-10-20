using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class DeviceHostMap : EntityTypeConfiguration<DeviceHost>
    {
        public DeviceHostMap()
        {
            //primary key configuration
            HasKey(e => e.Id);

            //primary key column configuration
            Property(e => e.Id)
                .HasColumnOrder(0)
                .HasColumnName("DeviceHostId");

            //device id column configuration
            Property(e => e.DeviceId)
                .HasColumnName(nameof(DeviceHost.DeviceId))
                .HasColumnOrder(1)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_HostDevice") { IsUnique = true, Order = 0 } }));

            //host id column configuration
            Property(e => e.HostId)
                .HasColumnName(nameof(DeviceHost.HostId))
                .HasColumnOrder(2)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_HostDevice") { IsUnique = true, Order = 1 } }));

            //host navigational mappings
            HasRequired(e => e.Host)
                .WithMany(e => e.Devices)
                .HasForeignKey(e => e.HostId);

            //device navigational mappings
            HasRequired(e => e.Device)
                .WithMany(e => e.Hosts)
                .HasForeignKey(e => e.DeviceId);

            //table configuration
            ToTable(nameof(DeviceHost));
        }
    }
}
