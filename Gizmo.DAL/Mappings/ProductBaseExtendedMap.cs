using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{  
    public class ProductBaseExtendedMap : EntityTypeConfiguration<ProductBaseExtended>
    {
        public ProductBaseExtendedMap()
        {
            this.ToTable("ProductBaseExtended");
        }        
    }
}
