using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AppGroupAppMap : EntityTypeConfiguration<AppGroupApp>
    {
        public AppGroupAppMap()
        {
            // Primary Key
            HasKey(t => new { t.AppGroupId, t.AppId });

            // Properties
            Property(x => x.AppGroupId)
                .HasColumnOrder(0);

            Property(t => t.AppId)
                .HasColumnOrder(1);
            
            // Table & Column Mappings
            ToTable("AppGroupApp");

            HasRequired(x => x.AppGroup)
                .WithMany(x => x.Apps)
                .HasForeignKey(x => x.AppGroupId);

            HasRequired(x => x.App)
                .WithMany(x=>x.AppGroups)
                .HasForeignKey(x => x.AppId)
                .WillCascadeOnDelete(false);
        }
    }
}
