using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class ProductTimePeriodDayTimeMap : IEntityTypeConfiguration<ProductTimePeriodDayTime>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductTimePeriodDayTime> builder)
        {
            // Key
            builder.HasKey(x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });

            // Indexes
            builder.HasIndex(t => t.PeriodDayId);

            // Table & Column Mappings
            builder.ToTable("ProductTimePeriodDayTime");

            builder.HasOne(x => x.Day)
                .WithMany(x => x.Times)
                .HasForeignKey(x => x.PeriodDayId);
        }
    }
}
