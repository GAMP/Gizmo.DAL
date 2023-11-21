using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ProductTimeMap : EntityTypeConfiguration<ProductTime>
    {
        public ProductTimeMap()
        {
            // Table & Column Mappings
            ToTable("ProductTime");

            HasOptional(x => x.AppGroup)
                .WithMany(x => x.TimeOffers)
                .HasForeignKey(x => x.AppGroupId);
        }
    }
}
