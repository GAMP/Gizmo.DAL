using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class ProductOLTimeMap : IEntityTypeConfiguration<ProductOLTime>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductOLTime> builder)
        {
            builder.ToTable(nameof(ProductOLTime));

            builder.HasOne(x => x.ProductTime)
                .WithMany(x => x.OrderLines)
                .HasForeignKey(x => x.ProductTimeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
