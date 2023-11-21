using Gizmo.DAL.Entities;

using GizmoDALV2;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class NewsMap : EntityTypeConfiguration<News>
    {
        public NewsMap()
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

            Property(t => t.Data)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.NORMAL);

            Property(t => t.Url)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.MediaUrl)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            Property(x => x.StartDate)
                .IsOptional()
                .HasColumnOrder(5);

            Property(x => x.EndDate)
                .IsOptional()
                .HasColumnOrder(6);

            Property(x => x.BackgroundUrl)
                .IsOptional()
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(7);

            Property(x => x.Options)
                .HasColumnOrder(8);

            // Table & Column Mappings
            ToTable("News");

            Property(t => t.Id)
                .HasColumnName("NewsId");
        }
    }
}
