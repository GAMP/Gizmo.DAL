using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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

            // Seeds
            builder.HasData(new ProductGroup() { Id = 1, Name = "Time Offers", DisplayOrder = 0, Guid = new Guid("e798a7fb-448b-4825-8b32-c5ea6db70271") });
            builder.HasData(new ProductGroup() { Id = 2, Name = "Food", DisplayOrder = 1, Guid = new Guid("e798a7fb-448b-4825-8b32-c5ea6db70272") });
            builder.HasData(new ProductGroup() { Id = 3, Name = "Drinks", DisplayOrder = 2, Guid = new Guid("e798a7fb-448b-4825-8b32-c5ea6db70273") });
            builder.HasData(new ProductGroup() { Id = 4, Name = "Sweets", DisplayOrder = 3, Guid = new Guid("e798a7fb-448b-4825-8b32-c5ea6db70274") });

        }
    }
}
