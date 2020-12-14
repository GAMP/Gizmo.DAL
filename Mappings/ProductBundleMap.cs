using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class ProductBundleMap : EntityTypeConfiguration<ProductBundle>
    {
        public ProductBundleMap()
        {
            // Relations
            this.ToTable("ProductBundle");          
        }
    }
}