using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class NewsMap : IEntityTypeConfiguration<News>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<News> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.Data)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.NORMAL);

            builder.Property(t => t.Url)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.MediaUrl)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.StartDate)
                .HasColumnOrder(5);

            builder.Property(x => x.EndDate)
                .HasColumnOrder(6);

            // Table & Column Mappings
            builder.ToTable("News");

            builder.Property(t => t.Id)
                .HasColumnName("NewsId");
        }
    }
}
