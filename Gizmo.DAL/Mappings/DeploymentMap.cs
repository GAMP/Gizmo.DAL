using Gizmo.DAL.Entities;

using Gizmo.DAL;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class DeploymentMap : EntityTypeConfiguration<Deployment>
    {
        public DeploymentMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id).
                HasColumnOrder(0);

            Property(t => t.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            Property(t => t.Source)
                .HasColumnOrder(2)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.Destination)
                .HasColumnOrder(3)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.ExcludeDirectories)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.NORMAL);

            Property(t => t.ExcludeFiles)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.NORMAL);

            Property(t => t.IncludeDirectories)
                .HasColumnOrder(6)
                .HasMaxLength(SQLStringSize.NORMAL);

            Property(t => t.IncludeFiles)
                .HasColumnOrder(7)
                .HasMaxLength(SQLStringSize.NORMAL);

            Property(t => t.RegistryString)
                .HasColumnOrder(8)
                .HasMaxLength(SQLStringSize.MEDIUM);

            Property(t => t.Guid)
                .HasColumnOrder(9)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true }
                }));

            Property(t => t.ComparisonLevel)
                .HasColumnOrder(10);

            Property(t => t.Options)
                .HasColumnOrder(11);

            // Table & Column Mappings
            ToTable("Deployment");

            Property(t => t.Id)
                .HasColumnName("DeploymentId");
        }
    }
}
