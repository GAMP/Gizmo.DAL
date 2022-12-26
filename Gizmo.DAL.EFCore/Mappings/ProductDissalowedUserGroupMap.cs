using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class ProductUserDisallowedMap : IEntityTypeConfiguration<ProductUserDisallowed>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductUserDisallowed> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("ProductUserDisallowed");

            builder.Property(x => x.Id)
                .HasColumnName("ProductUserDisallowedId")
                .HasColumnOrder(0);

            builder.Property(x => x.ProductId)
                .HasColumnOrder(1);

            builder.Property(x => x.UserGroupId)
                .HasColumnOrder(2);

            builder.Property(x => x.IsDisallowed)
                .HasColumnOrder(3);

            // Indexes
            builder.HasIndex(x => new { x.ProductId, x.UserGroupId }).HasDatabaseName("UQ_ProductUserGroup").IsUnique();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.DisallowedUserGroups)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.UserGroup)
                .WithMany(x => x.DissalowedProducts)
                .HasForeignKey(x => x.UserGroupId);
        }
    }
}
