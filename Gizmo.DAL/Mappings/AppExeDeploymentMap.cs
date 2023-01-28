using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class AppExeDeploymentMap : EntityTypeConfiguration<AppExeDeployment>
    {
        public AppExeDeploymentMap()
        {
            // Primary Key
            this.HasKey(t => new { AppExeId = t.AppExeId, DeploymentId = t.DeploymentId });

            // Properties
            this.Property(t => t.AppExeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DeploymentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("AppExeDeployment");

            // Ignores
            this.Ignore(x => x.Id);

            // Relationships            
            this.HasRequired(t => t.Deployment)
                .WithMany(t => t.DependentAppExes)
                .HasForeignKey(d => d.DeploymentId);

            this.HasRequired(t => t.AppExe)
                .WithMany(t => t.Deployments)
                .HasForeignKey(d => d.AppExeId);
        }
    }
}
