using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class DeploymentDeploymentMap : EntityTypeConfiguration<DeploymentDeployment>
    {
        public DeploymentDeploymentMap()
        {
            // Primary Key
            HasKey(t => new { ParentId = t.ParentId, ChildId = t.ChildId });

            // Properties
            Property(t => t.ParentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.ChildId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("DeploymentDeployment");

            // Ignores
            Ignore(x => x.Id);

            // Relationships
            HasRequired(t => t.Child)
                .WithMany(t => t.Dependent)
                .HasForeignKey(d => d.ChildId)
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Parent)
                .WithMany(t => t.Dependencies)
                .HasForeignKey(d => d.ParentId)
                .WillCascadeOnDelete(false);
        }
    }
}
