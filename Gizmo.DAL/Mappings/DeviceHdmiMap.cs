using Gizmo.DAL.Entities;
using GizmoDALV2;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class DeviceHdmiMap : EntityTypeConfiguration<DeviceHdmi>
    {
        public DeviceHdmiMap()
        {
            //serial number column configuration
            Property(x => x.UniqueId)
                .HasColumnOrder(3)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_UniqueId") { IsUnique = true }
                }));

            //table configuration
            ToTable(nameof(DeviceHdmi));
        }
    }
}
