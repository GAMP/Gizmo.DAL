using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// User member entity map.
    /// </summary>
    public class UserMemberMap : IEntityTypeConfiguration<UserMember>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserMember> builder)
        {
            builder.Property(userMember => userMember.Id)
                  .HasColumnOrder(0);

            builder.Property(userMember => userMember.Username)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(userMember => userMember.Email)
                .HasMaxLength(254);

            // Indexes
            builder.HasIndex(userMember => userMember.Username)
                .IsUnique()
                .HasFilter(null);

            builder.HasIndex(userMember => userMember.Email).IsUnique();
            
            builder.HasIndex(userMember => userMember.Id);

            // Table & Column Mappings
            builder.ToTable(nameof(UserMember));

            // Relationships
            builder.HasOne(userMember => userMember.UserGroup)
                .WithMany(userGroup => userGroup.Users)
                .HasForeignKey(userMember => userMember.UserGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
