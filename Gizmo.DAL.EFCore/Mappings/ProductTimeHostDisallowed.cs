using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class ProductTimeHostDisallowedMap : IEntityTypeConfiguration<ProductTimeHostDisallowed>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductTimeHostDisallowed> builder)
        {
            //key
            builder.HasKey(x => x.Id);

            builder.ToTable("ProductTimeHostDisallowed");

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("ProductTimeHostDisallowedId");

            builder.Property(x => x.ProductTimeId)
                .HasColumnOrder(1);

            builder.Property(x => x.HostGroupId)
                .HasColumnOrder(2);

            builder.Property(x => x.IsDisallowed)
                .HasColumnOrder(3);

            // Indexes
            builder.HasIndex(x => new { x.ProductTimeId, x.HostGroupId }).HasDatabaseName("UQ_ProductTimeHostGroup").IsUnique();

            builder.HasOne(x => x.ProductTime)
                .WithMany(x => x.DisallowedHostsGroup)
                .HasForeignKey(x => x.ProductTimeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.HostGroup)
                .WithMany(x => x.DisallowedTimeOffers)
                .HasForeignKey(x => x.HostGroupId);
        }
    }
}
