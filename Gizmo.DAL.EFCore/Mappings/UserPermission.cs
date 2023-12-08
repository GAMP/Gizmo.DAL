using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
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
            builder.HasIndex(x => new { x.UserId, x.Type, x.Value }).IsUnique();

            // Relations
            builder.HasOne(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Permissions);
        }
    }
}
