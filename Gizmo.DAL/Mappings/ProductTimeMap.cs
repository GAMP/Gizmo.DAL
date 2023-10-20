using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class ProductTimeMap : EntityTypeConfiguration<ProductTime>
    {
        public ProductTimeMap()
        {
            // Table & Column Mappings
            this.ToTable("ProductTime");

            this.HasOptional(x => x.AppGroup)
                .WithMany(x => x.TimeOffers)
                .HasForeignKey(x => x.AppGroupId);
        }
    }
}
