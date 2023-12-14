﻿using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class AssetTypeMap : IEntityTypeConfiguration<AssetType>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AssetType> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("AssetTypeId")
                .HasColumnOrder(0);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(t => t.Description)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.NORMAL);

            builder.Property(t => t.PartNumber)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            // Indexes
            builder.HasIndex(t => t.Name).IsUnique();

            // Table & Column Mappings
            builder.ToTable(nameof(AssetType));

            builder.HasMany(x => x.Assets)
                .WithOne(x => x.AssetType)
                .HasForeignKey(x => x.AssetTypeId);
        }
    }
}