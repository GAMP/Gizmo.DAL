﻿using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class HostLayoutGroupMap : IEntityTypeConfiguration<HostLayoutGroup>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<HostLayoutGroup> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("HostLayoutGroupId")
                .HasColumnOrder(0);

            builder.Property(x => x.Name)
                .HasMaxLength(GizmoDALV2.SQLStringSize.TINY45)
                .HasColumnOrder(1);

            builder.Property(x => x.DisplayOrder)
                .HasColumnOrder(2);

            // Indexes
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();

            // Table & Column Mappings
            builder.ToTable("HostLayoutGroup");

            // Seeds
            var hostLayoutGroup = new HostLayoutGroup()
            {
                Id  = 1,
                Name = "Default",
                DisplayOrder = 0
            };
            builder.HasData(hostLayoutGroup);
        }
    }
}