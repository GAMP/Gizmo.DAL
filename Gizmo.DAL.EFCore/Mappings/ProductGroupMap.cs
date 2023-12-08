using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
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
            builder.HasIndex(t => t.Name).IsUnique();

            builder.HasMany(x => x.ChildGroups)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ParentId);
        }
    }
}
