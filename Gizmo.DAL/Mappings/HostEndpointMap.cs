using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
