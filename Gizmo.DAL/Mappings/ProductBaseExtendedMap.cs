using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

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
