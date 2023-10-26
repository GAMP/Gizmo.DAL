﻿using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class UserMemberMap : IEntityTypeConfiguration<UserMember>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserMember> builder)
        {
            builder.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(t => t.Email)
                .HasMaxLength(254);

            // Indexes
            builder.HasIndex(t => t.Username).HasDatabaseName("UQ_Username").IsUnique().HasFilter(null);

            builder.HasIndex(t => t.Email).HasDatabaseName("UQ_Email").IsUnique();
            
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable(nameof(UserMember));

            // Relationships
            builder.HasOne(t => t.UserGroup)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seeds
            builder.HasData(new UserMember() { Id = 2, Username = "User", UserGroupId = 1, Guid = new Guid("38753737-24f1-40d7-8ac4-ba61660d666a") });
        }
    }
}