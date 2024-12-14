using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// User permission set map.
    /// </summary>
    public sealed class UserPermissionSetMap : IEntityTypeConfiguration<UserPermissionSet>
    {
        ///<inheritdoc/>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserPermissionSet> builder)
        {
            builder.ToTable(nameof(UserPermissionSet));

            builder.HasKey(permissionSet => permissionSet.Id);

            builder.Property(permissionSet => permissionSet.Id)
                .HasColumnName("UserPermissionSetId")
                .HasColumnOrder(0);

            builder.Property(permissionSet => permissionSet.Name)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired();

            builder.HasIndex(permissionSet => permissionSet.Name)
                .IsUnique();

            builder.HasOne(permissionSet => permissionSet.CreatedBy)
                .WithMany()
                .HasForeignKey(permissionSet => permissionSet.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(permissionSet => permissionSet.ModifiedBy)
                .WithMany()
                .HasForeignKey(permissionSet => permissionSet.ModifiedById)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
