using GizmoDALV2;
using GizmoDALV2.Entities;
using IntegrationLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Gizmo.DAL.EFCore
{
    public class UserPermissionMap : IEntityTypeConfiguration<UserPermission>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.ToTable(nameof(UserPermission));

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("UserPermissionId");

            builder.Property(x => x.UserId)
                .HasColumnOrder(1);

            builder.Property(x => x.Type)
                .HasColumnOrder(2)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.Value)
                .HasColumnOrder(3)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            // Indexes
            builder.HasIndex(x => new { x.UserId, x.Type, x.Value }).HasDatabaseName("UQ_UserPermission").IsUnique();

            // Relations
            builder.HasOne(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Permissions);

            // Seeds
            var autoIncrementId = 1;
            var claims = ClaimTypeBase.GetClaimTypes().ToList();
            var permissions = new List<UserPermission>();
            
            foreach (var claim in claims)
                permissions.Add(new UserPermission() { Id = autoIncrementId++, UserId = 1, Type = claim.Resource, Value = claim.Operation });

            builder.HasData(permissions);
        }
    }
}
