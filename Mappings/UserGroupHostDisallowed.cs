using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class UserGroupHostDisallowedMap : EntityTypeConfiguration<UserGroupHostDisallowed>
    {
        public UserGroupHostDisallowedMap()
        {
            HasKey(x => x.Id);

            ToTable("UserGroupHostDisallowed");

            Property(x => x.Id)
                .HasColumnName("UserGroupHostDisallowedId")
                .HasColumnOrder(0);

            Property(x => x.UserGroupId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_UserGroupHostGroup") { IsUnique = true, Order = 0 } }));

            Property(x => x.HostGroupId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_UserGroupHostGroup") { IsUnique = true, Order = 1 } }));

            Property(x => x.IsDisallowed)
                .HasColumnOrder(3);

            HasRequired(x => x.UserGroup)
                .WithMany(x => x.DissalowedHostGroups)
                .HasForeignKey(x => x.UserGroupId);

            HasRequired(x => x.HostGroup)
                .WithMany(x => x.DisallowedUserGroups)
                .HasForeignKey(x => x.HostGroupId);
        }
    }
}
