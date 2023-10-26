using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AppExeDeploymentMap : EntityTypeConfiguration<AppExeDeployment>
    {
        public AppExeDeploymentMap()
        {
            // Primary Key
            HasKey(t => new { AppExeId = t.AppExeId, DeploymentId = t.DeploymentId });

            // Properties
            Property(t => t.AppExeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.DeploymentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("AppExeDeployment");

            // Ignores
            Ignore(x => x.Id);

            // Relationships            
            HasRequired(t => t.Deployment)
                .WithMany(t => t.DependentAppExes)
                .HasForeignKey(d => d.DeploymentId);

            HasRequired(t => t.AppExe)
                .WithMany(t => t.Deployments)
                .HasForeignKey(d => d.AppExeId);
        }
    }
}
