using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class DiscountBaseMap : IEntityTypeConfiguration<DiscountBase>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<DiscountBase> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnOrder(0)
                .HasColumnName("DiscountId");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Value)
                .HasColumnOrder(2);

            builder.Property(x => x.DiscountAmountType)
                .HasColumnOrder(3);            

            // Indexes
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();

            // Table & Column Mappings
            builder.ToTable("DiscountBase");

            // Relationships
        }
    }

    public class DiscountUserMap : IEntityTypeConfiguration<DiscountUser>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<DiscountUser> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnOrder(0)
                .HasColumnName("DiscountUserId");  

            // Table & Column Mappings
            builder.ToTable("DiscountUser");

            // Relationships

            builder.HasOne(x => x.UserGroup)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Discount)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }

    public class DiscountProductMap : IEntityTypeConfiguration<DiscountProduct>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<DiscountProduct> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnOrder(0)
                .HasColumnName("DiscountProductId");

            // Table & Column Mappings
            builder.ToTable("DiscountUser");

            // Relationships

            builder.HasOne(x => x.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Discount)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }

    public class DiscountProductGroupMap : IEntityTypeConfiguration<DiscountProductGroup>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<DiscountProductGroup> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnOrder(0)
                .HasColumnName("DiscountProductGroupId");

            // Table & Column Mappings
            builder.ToTable("DiscountUser");

            // Relationships

            builder.HasOne(x => x.ProductGroup)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Discount)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
