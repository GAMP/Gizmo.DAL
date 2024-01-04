using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class UserMemberMap : IEntityTypeConfiguration<UserMember>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserMember> builder)
        {
            builder.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(t => t.Email)
                .HasMaxLength(254);

            // Indexes
            builder.HasIndex(t => t.Username).IsUnique().HasFilter(null);

            builder.HasIndex(t => t.Email).IsUnique();
            
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable(nameof(UserMember));

            // Relationships
            builder.HasOne(t => t.UserGroup)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
