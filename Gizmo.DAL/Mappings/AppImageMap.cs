using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AppImageMap : EntityTypeConfiguration<AppImage>
    {
        public AppImageMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("AppId")
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(x => x.Image)
                .HasMaxLength(Gizmo.DAL.SQLByteArraySize.MEDIUM);

            // Table & Column Mappings
            ToTable("AppImage");

            HasRequired(x => x.App)
                .WithRequiredDependent(x => x.Image)
                .WillCascadeOnDelete(true);
        }
    }
}
