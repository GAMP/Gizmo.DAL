using Gizmo.DAL.Entities;

using Gizmo.DAL;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class UserPermissionMap : EntityTypeConfiguration<UserPermission>
    {
        public UserPermissionMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            ToTable(nameof(UserPermission));

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("UserPermissionId");

            Property(x => x.UserId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_UserPermission") { IsUnique = true, Order = 0 } }));

            Property(x => x.Type)
                .HasColumnOrder(2)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_UserPermission") { IsUnique = true, Order = 1 } })); ;

            Property(x => x.Value)
                .HasColumnOrder(3)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_UserPermission") { IsUnique = true, Order = 2 } }));

            HasRequired(x => x.User)
                .WithMany(x => x.Permissions);
        }
    }
}
