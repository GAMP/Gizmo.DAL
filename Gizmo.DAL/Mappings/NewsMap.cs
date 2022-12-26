using GizmoDALV2.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
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
                .HasColumnOrder(5);

            Property(x => x.EndDate)
                .HasColumnOrder(6);

            // Table & Column Mappings
            ToTable("News");

            Property(t => t.Id)
                .HasColumnName("NewsId");
        }
    }
}
