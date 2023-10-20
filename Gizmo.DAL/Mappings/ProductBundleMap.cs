using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
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