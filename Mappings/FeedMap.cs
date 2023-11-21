using Gizmo.DAL.Entities;

using GizmoDALV2;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class FeedMap : EntityTypeConfiguration<Feed>
    {
        public FeedMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(t => t.Title)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.Url)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            Property(x => x.Maximum)
                .HasColumnOrder(3);

            // Table & Column Mappings
            ToTable("Feed");

            Property(t => t.Id)
                .HasColumnName("FeedId");
        }
    }
}
