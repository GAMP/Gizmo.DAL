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
    public class DeploymentDeploymentMap : EntityTypeConfiguration<DeploymentDeployment>
    {
        public DeploymentDeploymentMap()
        {
            // Primary Key
            this.HasKey(t => new { ParentId = t.ParentId, ChildId = t.ChildId });

            // Properties
            this.Property(t => t.ParentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ChildId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DeploymentDeployment");

            // Ignores
            this.Ignore(x => x.Id);

            // Relationships
            this.HasRequired(t => t.Child)
                .WithMany(t => t.Dependent)
                .HasForeignKey(d => d.ChildId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Parent)
                .WithMany(t => t.Dependencies)
                .HasForeignKey(d => d.ParentId)
                .WillCascadeOnDelete(false);
        }
    }
}
