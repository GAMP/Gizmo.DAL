using Gizmo.DAL.Entities;

using GizmoDALV2;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AppCategoryMap : EntityTypeConfiguration<AppCategory>
    {
        public AppCategoryMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("AppCategoryId")
                .HasColumnOrder(0);

            Property(x => x.ParentId)
                .HasColumnOrder(1);

            Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY45);

            Property(t => t.Guid)
                .HasColumnOrder(3)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true }
                }));


            // Table & Column Mappings
            ToTable("AppCategory");


            // Relationships
            HasOptional(t => t.Parent)
                .WithMany(t => t.Children)
                .HasForeignKey(d => d.ParentId);
        }
    }
}
