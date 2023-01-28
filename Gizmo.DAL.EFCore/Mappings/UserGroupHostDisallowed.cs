using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class UserGroupHostDisallowedMap : IEntityTypeConfiguration<UserGroupHostDisallowed>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserGroupHostDisallowed> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("UserGroupHostDisallowed");

            builder.Property(x => x.Id)
                .HasColumnName("UserGroupHostDisallowedId")
                .HasColumnOrder(0);

            builder.Property(x => x.UserGroupId)
                .HasColumnOrder(1);

            builder.Property(x => x.HostGroupId)
                .HasColumnOrder(2);

            builder.Property(x => x.IsDisallowed)
                .HasColumnOrder(3);

            // Indexes
            builder.HasIndex(x => new { x.UserGroupId, x.HostGroupId }).HasDatabaseName("UQ_UserGroupHostGroup").IsUnique();

            builder.HasOne(x => x.UserGroup)
                .WithMany(x => x.DissalowedHostGroups)
                .HasForeignKey(x => x.UserGroupId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.HostGroup)
                .WithMany(x => x.DisallowedUserGroups)
                .HasForeignKey(x => x.HostGroupId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
