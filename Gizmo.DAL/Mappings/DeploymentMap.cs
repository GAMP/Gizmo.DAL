using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class DeploymentMap : EntityTypeConfiguration<Deployment>
    {
        public DeploymentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id).
                HasColumnOrder(0);

            this.Property(t => t.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            this.Property(t => t.Source)
                .HasColumnOrder(2)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.Destination)
                .HasColumnOrder(3)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.ExcludeDirectories)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.NORMAL);

            this.Property(t => t.ExcludeFiles)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.NORMAL);

            this.Property(t => t.IncludeDirectories)
                .HasColumnOrder(6)
                .HasMaxLength(SQLStringSize.NORMAL);

            this.Property(t => t.IncludeFiles)
                .HasColumnOrder(7)
                .HasMaxLength(SQLStringSize.NORMAL);

            this.Property(t => t.RegistryString)
                .HasColumnOrder(8)
                .HasMaxLength(SQLStringSize.MEDIUM);

            this.Property(t => t.Guid)
                .HasColumnOrder(9)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true }
                }));

            this.Property(t => t.ComparisonLevel)
                .HasColumnOrder(10);

            this.Property(t => t.Options)
                .HasColumnOrder(11);

            // Table & Column Mappings
            this.ToTable("Deployment");

            this.Property(t => t.Id)
                .HasColumnName("DeploymentId");
        }
    }
}
