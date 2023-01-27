﻿using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class VerificationEmailMap : IEntityTypeConfiguration<VerificationEmail>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<VerificationEmail> builder)
        {
            builder.ToTable(nameof(VerificationEmail));

            builder.Property(e => e.Id)
                .IsRequired();

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.Property(e => e.Email)
                .HasMaxLength(254)
                .IsRequired();
        }
    }
}
