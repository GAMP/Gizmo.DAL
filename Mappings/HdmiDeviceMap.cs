using Gizmo.DAL.Entities;
using GizmoDALV2;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class HdmiDeviceMap : EntityTypeConfiguration<HdmiDevice>
    {
        public HdmiDeviceMap()
        {
            //serial number column configuration
            Property(x => x.SerialNumber)
                .HasColumnOrder(0)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            //table configuration
            ToTable(nameof(HdmiDevice));
        }
    }
}
