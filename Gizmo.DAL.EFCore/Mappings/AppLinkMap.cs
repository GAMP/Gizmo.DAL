using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class AppLinkMap : IEntityTypeConfiguration<AppLink>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppLink> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(t => t.AppId)
                .HasColumnOrder(1);

            builder.Property(t => t.Caption)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.Description)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.Url)
                .IsRequired()
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.DisplayOrder)
                .HasColumnOrder(5);

            builder.Property(t => t.Guid)
                .HasColumnOrder(6);

            // Indexes
            builder.HasIndex(t => t.Guid).HasDatabaseName("UQ_Guid").IsUnique();

            // Table & Column Mappings
            builder.ToTable("AppLink");

            builder.Property(t => t.Id)
                .HasColumnName("AppLinkId");

            // Relationships
            builder.HasOne(t => t.App)
                .WithMany(t => t.AppLinks)
                .HasForeignKey(d => d.AppId);
        }
    }
}
