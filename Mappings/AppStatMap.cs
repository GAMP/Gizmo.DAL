using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AppStatMap : EntityTypeConfiguration<AppStat>
    {
        public AppStatMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.AppId)
                .HasColumnOrder(1);

            Property(x => x.AppExeId)
                .HasColumnOrder(2);

            Property(x => x.HostId)
                .HasColumnOrder(3);

            Property(x => x.UserId)
                .HasColumnOrder(4);

            Property(x => x.Span)
                .HasColumnOrder(5);

            Property(x => x.StartTime)
                .HasColumnOrder(6);

            // Table & Column Mappings
            ToTable("AppStat");

            Property(t => t.Id)
                .HasColumnName("AppStatId");

            // Relationships
            HasRequired(t => t.App)
                .WithMany(t => t.AppStats)
                .HasForeignKey(d => d.AppId);

            HasRequired(t => t.AppExe)
                .WithMany(t => t.AppStats)
                .HasForeignKey(d => d.AppExeId);

            HasRequired(t => t.Host)
                .WithMany(t => t.AppStats)
                .HasForeignKey(d => d.HostId);

            HasRequired(x => x.User)
                .WithMany(x => x.AppStats)
                .HasForeignKey(x => x.UserId);
        }
    }
}
