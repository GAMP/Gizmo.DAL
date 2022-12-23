using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class ProductPeriodMap : IEntityTypeConfiguration<ProductPeriod>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductPeriod> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Indexes
            builder.HasIndex(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            // Table & Column Mappings
            builder.ToTable("ProductPeriod");

            builder.Property(x => x.Id)
                .HasColumnName("ProductId");

            builder.HasOne(x => x.Product)
                .WithOne(x => x.Period)
                .HasForeignKey<ProductPeriod>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class ProductPeriodDayMap : IEntityTypeConfiguration<ProductPeriodDay>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductPeriodDay> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Table & Column Mappings
            builder.ToTable("ProductPeriodDay");

            builder.Property(x => x.Id)
                .HasColumnName("ProductPeriodDayId");

            builder.Property(x => x.ProductPeriodId)
                .HasColumnName("ProductPeriodId");
                
            // Indexes
            builder.HasIndex(x => new { x.ProductPeriodId, x.Day }).HasDatabaseName("UQ_ProductPeriodDay").IsUnique();

            builder.HasOne(x => x.Period)
                .WithMany(x => x.Days)
                .HasForeignKey(x => x.ProductPeriodId);
        }
    }

    public class ProductPeriodDayTimeMap : IEntityTypeConfiguration<ProductPeriodDayTime>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductPeriodDayTime> builder)
        {
            // Key
            builder.HasKey(x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });

            // Table & Column Mappings
            builder.ToTable("ProductPeriodDayTime");

            // Indexes
            builder.HasIndex(t => t.PeriodDayId);

            builder.HasOne(x => x.Day)
                .WithMany(x => x.Times)
                .HasForeignKey(x => x.PeriodDayId);
        }
    }
}
