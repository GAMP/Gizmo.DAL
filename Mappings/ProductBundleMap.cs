using Gizmo.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ProductBundleMap : EntityTypeConfiguration<ProductBundle>
    {
        public ProductBundleMap()
        {
            // Relations
            ToTable("ProductBundle");          
        }
    }
}