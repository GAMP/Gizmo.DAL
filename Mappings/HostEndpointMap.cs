using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class HostEndpointMap : EntityTypeConfiguration<HostEndpoint>
    {
        public HostEndpointMap()
        {
            // Table & Column Mappings
            ToTable("HostEndpoint");       
        }
    }
}
