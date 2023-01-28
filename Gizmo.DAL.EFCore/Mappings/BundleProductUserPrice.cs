using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class BundleProductUserPriceMap : IEntityTypeConfiguration<BundleProductUserPrice>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<BundleProductUserPrice> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("BundleProductUserPriceId")
                .HasColumnOrder(0);

            builder.Property(x => x.Price)
                .HasColumnOrder(3);

            // Indexes
            builder.HasIndex(t => new { t.BundleProductId, t.UserGroupId }).HasDatabaseName("UQ_BundleProductUserGroup").IsUnique();

            builder.ToTable(nameof(BundleProductUserPrice));

            // Relations            
            builder.HasOne(x => x.BundleProduct)
                .WithMany(x => x.UserPrices)
                .HasForeignKey(x => x.BundleProductId);

            builder.HasOne(x => x.UserGroup)
                .WithMany()
                .HasForeignKey(x => x.UserGroupId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
