﻿using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class UsageUserSessionMap : IEntityTypeConfiguration<UsageUserSession>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UsageUserSession> builder)
        {
            builder.ToTable(nameof(UsageUserSession));

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.UserSession)
                .WithMany(x => x.Usage)
                .HasForeignKey(x => x.UserSessionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}