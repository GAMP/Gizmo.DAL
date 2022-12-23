using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class ProductGroupMap : IEntityTypeConfiguration<ProductGroup>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductGroup> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            //Properties
            builder.Property(x => x.Name)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired();

            // Relations
            builder.ToTable("ProductGroup");

            builder.Property(x => x.Id)
                .HasColumnName("ProductGroupId");

            // Indexes
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();

            builder.HasMany(x => x.ChildGroups)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ParentId);
        }
    }
}
