using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class AppRatingMap : EntityTypeConfiguration<AppRating>
    {
        public AppRatingMap()
        {
            // Primary Key
            this.HasKey(x => new { x.AppId,x.UserId});

            // Properties
            this.Property(x => x.AppId)
                .HasColumnOrder(0);

            this.Property(x => x.UserId)
                .HasColumnOrder(1);

            this.Property(x => x.Value)
                .HasColumnOrder(2);

            this.Property(x => x.Date)
                .HasColumnOrder(3);

            // Table & Column Mappings
            this.ToTable("AppRating");
      
            // Relationships
            this.HasRequired(t => t.App)
                .WithMany(t => t.AppRatings)
                .HasForeignKey(d => d.AppId)
                .WillCascadeOnDelete(true);

            this.HasRequired(t => t.User)
               .WithMany(t => t.AppRatings)
               .HasForeignKey(d => d.UserId)
               .WillCascadeOnDelete(true);
        }
    }
}
