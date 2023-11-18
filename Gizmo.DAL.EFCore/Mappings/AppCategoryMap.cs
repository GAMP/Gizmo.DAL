using GizmoDALV2;
using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class AppCategoryMap : IEntityTypeConfiguration<AppCategory>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppCategory> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("AppCategoryId")
                .HasColumnOrder(0);

            builder.Property(x => x.ParentId)
                .HasColumnOrder(1);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(t => t.Guid)
                .HasColumnOrder(3);

            // Indexes
            builder.HasIndex(t => t.Guid).IsUnique();

            // Table & Column Mappings
            builder.ToTable("AppCategory");

            // Relationships
            builder.HasOne(t => t.Parent)
                .WithMany(t => t.Children)
                .HasForeignKey(d => d.ParentId);
        }
    }
}
