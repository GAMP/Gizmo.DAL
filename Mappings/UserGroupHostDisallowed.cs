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
    public class UserGroupHostDisallowedMap : EntityTypeConfiguration<UserGroupHostDisallowed>
    {
        public UserGroupHostDisallowedMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("UserGroupHostDisallowed");

            this.Property(x => x.Id)
                .HasColumnName("UserGroupHostDisallowedId")
                .HasColumnOrder(0);

            this.Property(x => x.UserGroupId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_UserGroupHostGroup") { IsUnique = true, Order = 0 } }));

            this.Property(x => x.HostGroupId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_UserGroupHostGroup") { IsUnique = true, Order = 1 } }));

            this.Property(x => x.IsDisallowed)
                .HasColumnOrder(3);

            this.HasRequired(x => x.UserGroup)
                .WithMany(x => x.DissalowedHostGroups)
                .HasForeignKey(x => x.UserGroupId);

            this.HasRequired(x => x.HostGroup)
                .WithMany(x => x.DisallowedUserGroups)
                .HasForeignKey(x => x.HostGroupId);
        }
    }
}
