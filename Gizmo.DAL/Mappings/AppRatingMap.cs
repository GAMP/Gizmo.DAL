using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AppRatingMap : EntityTypeConfiguration<AppRating>
    {
        public AppRatingMap()
        {
            // Primary Key
            HasKey(x => new { x.AppId, x.UserId });

            // Properties
            Property(x => x.AppId)
                .HasColumnOrder(0);

            Property(x => x.UserId)
                .HasColumnOrder(1);

            Property(x => x.Value)
                .HasColumnOrder(2);

            Property(x => x.Date)
                .HasColumnOrder(3);

            // Table & Column Mappings
            ToTable("AppRating");

            // Relationships
            HasRequired(t => t.App)
                .WithMany(t => t.AppRatings)
                .HasForeignKey(d => d.AppId)
                .WillCascadeOnDelete(true);

            HasRequired(t => t.User)
               .WithMany(t => t.AppRatings)
               .HasForeignKey(d => d.UserId)
               .WillCascadeOnDelete(true);
        }
    }
}
