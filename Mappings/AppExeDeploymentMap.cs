using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class AppExeDeploymentMap : IEntityTypeConfiguration<AppExeDeployment>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppExeDeployment> builder)
        {
            // Primary Key
            builder.HasKey(t => new { AppExeId = t.AppExeId, DeploymentId = t.DeploymentId });

            // Properties
            builder.Property(t => t.AppExeId).ValueGeneratedNever();

            builder.Property(t => t.DeploymentId).ValueGeneratedNever();

            // Table & Column Mappings
            builder.ToTable("AppExeDeployment");

            // Ignores
            builder.Ignore(x => x.Id);

            // Indexes
            builder.HasIndex(t => t.AppExeId);

            // Relationships            
            builder.HasOne(t => t.Deployment)
                .WithMany(t => t.DependentAppExes)
                .HasForeignKey(d => d.DeploymentId);

            builder.HasOne(t => t.AppExe)
                .WithMany(t => t.Deployments)
                .HasForeignKey(d => d.AppExeId);
        }
    }
}
