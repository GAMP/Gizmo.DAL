using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class ProductOLSessionMap : IEntityTypeConfiguration<ProductOLSession>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductOLSession> builder)
        {
            builder.ToTable(nameof(ProductOLSession));

            builder.HasOne(x => x.UsageSession)
                .WithMany()
                .HasForeignKey(x => x.UsageSessionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
