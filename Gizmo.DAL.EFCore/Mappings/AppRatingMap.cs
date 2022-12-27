using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class AppRatingMap : IEntityTypeConfiguration<AppRating>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppRating> builder)
        {
            // Primary Key
            builder.HasKey(x => new { x.AppId,x.UserId});

            // Properties
            builder.Property(x => x.AppId)
                .HasColumnOrder(0);

            builder.Property(x => x.UserId)
                .HasColumnOrder(1);

            builder.Property(x => x.Value)
                .HasColumnOrder(2);

            builder.Property(x => x.Date)
                .HasColumnOrder(3);

            // Table & Column Mappings
            builder.ToTable("AppRating");

            // Indexes
            builder.HasIndex(x => x.AppId);

            // Relationships
            builder.HasOne(t => t.App)
                .WithMany(t => t.AppRatings)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.User)
               .WithMany(t => t.AppRatings)
               .HasForeignKey(d => d.UserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
