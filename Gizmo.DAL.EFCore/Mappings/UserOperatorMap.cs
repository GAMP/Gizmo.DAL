using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class UserOperatorMap : IEntityTypeConfiguration<UserOperator>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserOperator> builder)
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
            builder.ToTable("UserOperator");

            // Seeds
            builder.HasData(new UserOperator
            {
                Id = 1,
                Username = "Admin",
                CreatedTime = new DateTime(2023, 01, 01),
                Guid = new Guid("691ea8b4-d794-4096-84ae-bbdb7bcc0b02"),
            });
        }
    }
}
