using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class HostLayoutGroupImageMap : EntityTypeConfiguration<HostLayoutGroupImage>
    {
        public HostLayoutGroupImageMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(x => x.Image)
                .HasMaxLength(Gizmo.DAL.SQLByteArraySize.MEDIUM);

            // Table & Column Mappings
            ToTable("HostLayoutGroupImage");

            Property(x => x.Id)
                .HasColumnName("HostLayoutGroupId");

            HasRequired(x => x.HostLayoutGroup)
                .WithRequiredDependent(x => x.Image)
                .WillCascadeOnDelete(true);
        }
    }
}
