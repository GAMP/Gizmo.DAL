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
    public class AppGroupAppMap : EntityTypeConfiguration<AppGroupApp>
    {
        public AppGroupAppMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AppGroupId, t.AppId });

            // Properties
            this.Property(x => x.AppGroupId)
                .HasColumnOrder(0);

            this.Property(t => t.AppId)
                .HasColumnOrder(1);
            
            // Table & Column Mappings
            this.ToTable("AppGroupApp");

            this.HasRequired(x => x.AppGroup)
                .WithMany(x => x.Apps)
                .HasForeignKey(x => x.AppGroupId);

            this.HasRequired(x => x.App)
                .WithMany(x=>x.AppGroups)
                .HasForeignKey(x => x.AppId)
                .WillCascadeOnDelete(false);
        }
    }
}
