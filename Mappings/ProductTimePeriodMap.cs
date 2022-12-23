using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
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

    public class ProductTimePeriodDayMap : IEntityTypeConfiguration<ProductTimePeriodDay>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductTimePeriodDay> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Indexes
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("ProductTimePeriodDay");

            builder.Property(x => x.Id)
                .HasColumnName("ProductTimePeriodDayId");

            builder.Property(x => x.ProductTimePeriodId)
                .HasColumnName("ProductTimePeriodId");
                
            // Indexes
            builder.HasIndex(x => new { x.ProductTimePeriodId, x.Day }, "UQ_ProductTimePeriodDay").IsUnique();

            builder.HasOne(x => x.Period)
                .WithMany(x => x.Days)
                .HasForeignKey(x => x.ProductTimePeriodId);
        }
    }

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
