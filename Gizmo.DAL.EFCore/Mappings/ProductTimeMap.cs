using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Gizmo.DAL.Mappings
{
    public class ProductTimeMap : IEntityTypeConfiguration<ProductTime>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductTime> builder)
        {
            // Table & Column Mappings
            builder.ToTable("ProductTime");

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.AppGroup)
                .WithMany(x => x.TimeOffers)
                .HasForeignKey(x => x.AppGroupId);

            // Seeds
            builder.HasData(new ProductTime
            {
                Id = 6,
                Minutes = 360,
                Price = 12,
                WeekEndMaxMinutes = null,
                Name = "Six Hours (6)",
                ProductGroupId = 1,
                Guid = new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba6"),
            });

            builder.HasData(new ProductTime
            {
                Id = 7,
                Minutes = 360,
                Price = 16,
                WeekEndMaxMinutes = null,
                Name = "Six Hours (6 Weekends)",
                ProductGroupId = 1,
                Guid = new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba7"),
            });
        }
    }
}
