using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class UserPermissionMap : EntityTypeConfiguration<UserPermission>
    {
        public UserPermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);           

            // Properties
            this.ToTable(nameof(UserPermission));

            this.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("UserPermissionId");

            this.Property(x => x.UserId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_UserPermission") { IsUnique = true, Order = 0 } }));

            this.Property(x => x.Type)
                .HasColumnOrder(2)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_UserPermission") { IsUnique = true, Order = 1 } })); ;

            this.Property(x => x.Value)
                .HasColumnOrder(3)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_UserPermission") { IsUnique = true, Order = 2 } }));

            this.HasRequired(x => x.User)
                .WithMany(x => x.Permissions);
        }
    }
}
