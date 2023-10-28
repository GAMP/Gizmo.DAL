using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class ProductTimePeriodMap : IEntityTypeConfiguration<ProductTimePeriod>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductTimePeriod> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            // Indexes
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("ProductTimePeriod");

            builder.Property(x => x.Id)
                .HasColumnName("ProductId");

            builder.HasOne(x => x.ProductTime)
                .WithOne(x => x.UsePeriod)
                .HasForeignKey<ProductTimePeriod>(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Seeds
            builder.HasData(new ProductTimePeriod() { Id = 6 });
        }
    }
}
