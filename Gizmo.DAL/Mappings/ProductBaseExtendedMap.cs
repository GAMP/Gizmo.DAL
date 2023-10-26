using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ProductBaseExtendedMap : EntityTypeConfiguration<ProductBaseExtended>
    {
        public ProductBaseExtendedMap()
        {
            ToTable("ProductBaseExtended");
        }
    }
}
