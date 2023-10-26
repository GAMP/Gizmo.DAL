using Gizmo.DAL.Entities;

using GizmoDALV2;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AppLinkMap : EntityTypeConfiguration<AppLink>
    {
        public AppLinkMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(t => t.AppId)
                .HasColumnOrder(1);

            Property(t => t.Caption)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.Description)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.Url)
                .IsRequired()
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            Property(x => x.DisplayOrder)
                .HasColumnOrder(5);

            Property(t => t.Guid)
                .HasColumnOrder(6)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true }
                }));

            // Table & Column Mappings
            ToTable("AppLink");

            Property(t => t.Id)
                .HasColumnName("AppLinkId");

            // Relationships
            HasRequired(t => t.App)
                .WithMany(t => t.AppLinks)
                .HasForeignKey(d => d.AppId);
        }
    }
}
