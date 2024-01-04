using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class DeploymentDeploymentMap : IEntityTypeConfiguration<DeploymentDeployment>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<DeploymentDeployment> builder)
        {
            // Primary Key
            builder.HasKey(t => new { ParentId = t.ParentId, ChildId = t.ChildId });

            // Properties
            builder.Property(t => t.ParentId)
                .ValueGeneratedNever();

            builder.Property(t => t.ChildId)
                .ValueGeneratedNever();

            // Table & Column Mappings
            builder.ToTable("DeploymentDeployment");

            // Indexes
            builder.HasIndex(x => x.ParentId);

            // Ignores
            builder.Ignore(x => x.Id);

            // Relationships
            builder.HasOne(t => t.Child)
                .WithMany(t => t.Dependent)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Parent)
                .WithMany(t => t.Dependencies)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
