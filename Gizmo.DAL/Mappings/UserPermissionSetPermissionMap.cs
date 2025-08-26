using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// User permission set permission map.
    /// </summary>
    public sealed class UserPermissionSetPermissionMap : IEntityTypeConfiguration<UserPermissionSetPermission>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<UserPermissionSetPermission> builder)
        {
            builder.ToTable(nameof(UserPermissionSetPermission));

            builder.HasKey(userPermissionSetPermission => userPermissionSetPermission.Id);
            builder.Property(userPermissionSetPermission => userPermissionSetPermission.Id)
                .HasColumnName("UserPermissionSetPermissionId")
                .HasColumnOrder(0);

            builder.Property(userPermissionSetPermission => userPermissionSetPermission.PermissionSetId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(userPermissionSetPermission => userPermissionSetPermission.Type)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired();

            builder.Property(userPermissionSetPermission => userPermissionSetPermission.Value)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired();

            builder.HasIndex(UserPermissionSetPermission => new { UserPermissionSetPermission.PermissionSetId, UserPermissionSetPermission.Type, UserPermissionSetPermission.Value })
                .IsUnique()
                .HasFilter(null);

            builder.HasOne(userPermissionSetPermission => userPermissionSetPermission.PermissionSet)
                .WithMany(userPermissionSet => userPermissionSet.Permissions)
                .HasForeignKey(userPermissionSetPermission => userPermissionSetPermission.PermissionSetId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
