using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Discount entity map.
    /// </summary>
    public sealed class DiscountMap : IEntityTypeConfiguration<Discount>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable(nameof(Discount))
                   .UseTptMappingStrategy();

            builder.HasKey(discountBase => discountBase.Id);

            builder.Property(discountBase => discountBase.Id)
                .HasColumnOrder(0)
                .HasColumnName("DiscountId");

            builder.Property(discountBase => discountBase.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(discountBase => discountBase.Description)
                .IsRequired(false)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            builder.HasIndex(discountBase => discountBase.Name).IsUnique();
        }
    }

    /// <summary>
    /// Discount basic entity map.
    /// </summary>
    public sealed class DiscountBasicMap : IEntityTypeConfiguration<DiscountBasic>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DiscountBasic> builder)
        {
            builder.ToTable(nameof(DiscountBasic))
                .HasBaseType<Discount>();
        }
    }

    /// <summary>
    /// Discount group entity map.
    /// </summary>
    public sealed class DiscountGroupMap : IEntityTypeConfiguration<DiscountGroup>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DiscountGroup> builder)
        {
            builder.HasKey(discountGroup => discountGroup.Id);

            builder.Property(discountGroup => discountGroup.Id)
                .HasColumnName("DiscountGroupId")
                .HasColumnOrder(0);

            builder.Property(discountGroup => discountGroup.Name)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired();

            builder.HasIndex(discountGroup => discountGroup.Name).IsUnique();
        }
    }

    /// <summary>
    /// Discount group discount entity map.
    /// </summary>
    public sealed class DiscountGroupDiscountMap : IEntityTypeConfiguration<DiscountGroupDiscount>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DiscountGroupDiscount> builder)
        {
            builder.HasKey(discountGroupDiscount => discountGroupDiscount.Id);

            builder.Property(discountGroupDiscount => discountGroupDiscount.Id)
                .HasColumnName("DiscountGroupDiscountId");

            builder.HasOne(discountGroupDiscount => discountGroupDiscount.DiscountGroup)
                .WithMany(discountGroup => discountGroup.Discounts)
                .HasForeignKey(discountGroupDiscount => discountGroupDiscount.DiscountGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(target => new { target.DiscountGroupId, target.DiscountId }).IsUnique();
        }
    }

    /// <summary>
    /// Discount target group entity map.
    /// </summary>
    public sealed class TargetGroupMap : IEntityTypeConfiguration<TargetGroup>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetGroup> builder)
        {
            builder.ToTable(nameof(TargetGroup));

            builder.HasKey(targetGroup => targetGroup.Id);

            builder.Property(targetGroup => targetGroup.Id)
                .HasColumnName("TargetGroupId");

            builder.HasMany(targetGroup => targetGroup.Targets)
                .WithOne(target => target.TargetGroup);
        }
    }

    /// <summary>
    /// Target group product map.
    /// </summary>
    public sealed class TargetGroupProductMap : IEntityTypeConfiguration<TargetGroupProduct>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetGroupProduct> builder)
        {
            builder.ToTable(nameof(TargetGroupProduct))
                .HasBaseType<TargetGroup>();
        }
    }

    /// <summary>
    /// Target group product time map.
    /// </summary>
    public sealed class TargetGroupProductTimeMap : IEntityTypeConfiguration<TargetGroupProductTime>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetGroupProductTime> builder)
        {
            builder.ToTable(nameof(TargetGroupProductTime))
                .HasBaseType<TargetGroup>();
        }
    }

    /// <summary>
    /// Target group product group map.
    /// </summary>
    public sealed class TargetGroupProductGroupMap : IEntityTypeConfiguration<TargetGroupProductGroup>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetGroupProductGroup> builder)
        {
            builder.ToTable(nameof(TargetGroupProductGroup))
                .HasBaseType<TargetGroup>();
        }
    }

    /// <summary>
    /// Target group bill profile map.
    /// </summary>
    public sealed class TargetGroupBillProfileMap : IEntityTypeConfiguration<TargetGroupBillProfile>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetGroupBillProfile> builder)
        {
            builder.ToTable(nameof(TargetGroupBillProfile))
                .HasBaseType<TargetGroup>();
        }
    }

    /// <summary>
    /// Target group target entity map.
    /// </summary>
    public sealed class TargetMap : IEntityTypeConfiguration<Target>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Target> builder)
        {
            builder.ToTable(nameof(Target))
                .UseTptMappingStrategy();

            builder.HasKey(target => target.Id);

            builder.Property(target => target.Id)
                .HasColumnName("TargetId");

            builder.HasOne(target => target.TargetGroup)
                .WithMany(targetGroup => targetGroup.Targets)
                .HasForeignKey(target => target.TargetGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    /// <summary>
    /// Target group target product entity map.
    /// </summary>
    public sealed class TargetProductMap : IEntityTypeConfiguration<TargetProduct>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetProduct> builder)
        {
            builder.ToTable(nameof(TargetProduct))
                .HasBaseType<Target>();

            builder.HasOne(target => target.Product)
                .WithMany()
                .HasForeignKey(target => target.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(target => target.TargetGroupProduct)
                .WithMany(targetGroup => targetGroup.Products)
                .HasForeignKey(target => target.TargetGroupProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(target => new { target.TargetGroupProductId, target.ProductId }).IsUnique();
        }
    }

    /// <summary>
    /// Target group target product time entity map.
    /// </summary>
    public sealed class TargetProductTimeMap : IEntityTypeConfiguration<TargetProductTime>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetProductTime> builder)
        {
            builder.ToTable(nameof(TargetProductTime))
                .HasBaseType<Target>();

            builder.HasOne(target => target.ProductTime)
                .WithMany()
                .HasForeignKey(target => target.ProductTimeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(target => target.TargetGroupProductTime)
                .WithMany(targetGroup => targetGroup.ProductTimes)
                .HasForeignKey(target => target.TargetGroupProductTimeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(target => new { target.TargetGroupProductTimeId, target.ProductTimeId }).IsUnique();
        }
    }

    /// <summary>
    /// Target group target product group entity map.
    /// </summary>
    public sealed class TargetProductGroupMap : IEntityTypeConfiguration<TargetProductGroup>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetProductGroup> builder)
        {
            builder.ToTable(nameof(TargetProductGroup))
                .HasBaseType<Target>();

            builder.HasOne(target => target.ProductGroup)
                .WithMany()
                .HasForeignKey(target => target.ProductGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(target => target.TargetGroupProductGroup)
                .WithMany(targetGroup => targetGroup.ProductGroups)
                .HasForeignKey(target => target.TargetGroupProductGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(target => new { target.TargetGroupProductGroupId, target.ProductGroupId }).IsUnique();
        }
    }

    /// <summary>
    /// Target group target billing profile entity map.
    /// </summary>
    public sealed class TargetBillProfileMap : IEntityTypeConfiguration<TargetBillProfile>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetBillProfile> builder)
        {
            builder.ToTable(nameof(TargetBillProfile))
                .HasBaseType<Target>();

            builder.HasOne(target => target.BillProfile)
                .WithMany()
                .HasForeignKey(target => target.BillProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(target => target.TargetGroupBillProfile)
                .WithMany(targetGroup => targetGroup.BilliProfiles)
                .HasForeignKey(target => target.TargetGroupBillProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(target => new { target.TargetGroupBillProfileId, target.BillProfileId }).IsUnique();
        }
    }
}
