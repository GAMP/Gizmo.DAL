using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class AppMap : IEntityTypeConfiguration<App>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<App> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnName("AppId")
                .HasColumnOrder(0);

            builder.Property(t => t.Title)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.PublisherId)
                .HasColumnOrder(2);

            builder.Property(t => t.DeveloperId)
                .HasColumnOrder(3);

            builder.Property(t => t.AppCategoryId)
                .HasColumnOrder(4);

            builder.Property(t => t.Description)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.NORMAL);

            builder.Property(t => t.ReleaseDate)
                .HasColumnOrder(6);

            builder.Property(t => t.Version)
                .HasColumnOrder(7)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(t => t.Options)
                .HasColumnOrder(8);

            builder.Property(t => t.AgeRating)
                .HasColumnOrder(9);

            builder.Property(t => t.Guid)
                .HasColumnOrder(10);

            builder.Property(t => t.DefaultExecutableId)
                .HasColumnOrder(11);

            // Indexes
            builder.HasIndex(t => t.Guid).HasDatabaseName("UQ_Guid").IsUnique();

            // Table & Column Mappings
            builder.ToTable("App");

            // Relationships
            builder.HasOne(t => t.AppCategory)
                .WithMany(t => t.Apps)
                .HasForeignKey(d => d.AppCategoryId);

            builder.HasOne(t => t.Developer)
                .WithMany(t => t.DevelopedApps)
                .HasForeignKey(d => d.DeveloperId);

            builder.HasOne(t => t.Publisher)
                .WithMany(t => t.PublishedApps)
                .HasForeignKey(d => d.PublisherId);
        }
    }
}
