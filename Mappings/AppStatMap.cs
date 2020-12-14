using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class AppStatMap : EntityTypeConfiguration<AppStat>
    {
        public AppStatMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(x => x.AppId)
                .HasColumnOrder(1);

            this.Property(x => x.AppExeId)
                .HasColumnOrder(2);

            this.Property(x => x.HostId)
                .HasColumnOrder(3);

            this.Property(x => x.UserId)
                .HasColumnOrder(4);

            this.Property(x => x.Span)
                .HasColumnOrder(5);

            this.Property(x => x.StartTime)
                .HasColumnOrder(6);

            // Table & Column Mappings
            this.ToTable("AppStat");

            this.Property(t => t.Id)
                .HasColumnName("AppStatId");

            // Relationships
            this.HasRequired(t => t.App)
                .WithMany(t => t.AppStats)
                .HasForeignKey(d => d.AppId);

            this.HasRequired(t => t.AppExe)
                .WithMany(t => t.AppStats)
                .HasForeignKey(d => d.AppExeId);

            this.HasRequired(t => t.Host)
                .WithMany(t => t.AppStats)
                .HasForeignKey(d => d.HostId);

            this.HasRequired(x => x.User)
                .WithMany(x => x.AppStats)
                .HasForeignKey(x => x.UserId);
        }
    }
}
