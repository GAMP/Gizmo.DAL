using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class HostEndpointMap : EntityTypeConfiguration<HostEndpoint>
    {
        public HostEndpointMap()
        {
            // Table & Column Mappings
            this.ToTable("HostEndpoint");
        }
    }
}
