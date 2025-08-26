using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class SecurityProfilePolicyMap : IEntityTypeConfiguration<SecurityProfilePolicy>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<SecurityProfilePolicy> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.SecurityProfileId)
                .HasColumnOrder(1);

            builder.Property(x => x.Type)
                .HasColumnOrder(2);

            builder.Property(t => t.Id)
                .HasColumnName("SecurityProfilePolicyId");

            // Indexes
            builder.HasIndex(x => new { x.SecurityProfileId, x.Type }).IsUnique();

            // Table & Column Mappings
            builder.ToTable("SecurityProfilePolicy");

            // Relationships
            builder.HasOne(t => t.SecurityProfile)
                .WithMany(t => t.Policies)
                .HasForeignKey(d => d.SecurityProfileId);
        }
    }
}
