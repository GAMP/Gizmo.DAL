using Gizmo.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class HostDeviceMap : EntityTypeConfiguration<HostDevice>
    {
        public HostDeviceMap()
        {
            //primary key configuration
            HasKey(e => e.Id);

            //primary key column configuration
            Property(e => e.Id)
                .HasColumnOrder(0)
                .HasColumnName("HostDeviceId");

            //host id column configuration
            Property(e => e.HostId)
                .HasColumnName(nameof(HostDevice.HostId))
                .HasColumnOrder(1)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_HostDevice") { IsUnique = true, Order = 0 } }));
            
            //device id column configuration
            Property(e => e.DeviceId)
                .HasColumnName(nameof(HostDevice.DeviceId))
                .HasColumnOrder(2)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_HostDevice") { IsUnique = true, Order = 0 } }));

            //table configuration
            ToTable(nameof(HostDevice));
        }
    }
}
