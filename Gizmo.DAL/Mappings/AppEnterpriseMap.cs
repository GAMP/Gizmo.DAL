using Gizmo.DAL.Entities;

using GizmoDALV2;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AppEnterpriseMap : EntityTypeConfiguration<AppEnterprise>
    {
        public AppEnterpriseMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(t => t.Id)
              .HasColumnName("AppEnterpriseId");

            Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            Property(t => t.Guid)
                .HasColumnOrder(2)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true }
                }));

            // Table & Column Mappings
            ToTable("AppEnterprise");
        }
    }
}
