using Gizmo.DAL.Entities;
using GizmoDALV2;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class DeviceMap : EntityTypeConfiguration<Device>
    {
        public DeviceMap()
        {
            //primary key configuration
            HasKey(e => e.Id);

            //primary key column configuration
            Property(e => e.Id)
                .HasColumnOrder(0)
                .HasColumnName("DeviceId");

            //device name configuration
            Property(e => e.Name)
                .HasColumnOrder(1)
                .HasColumnName(nameof(Device.Name))
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            //table name configuration
            ToTable(nameof(Device));
        }
    }
}
