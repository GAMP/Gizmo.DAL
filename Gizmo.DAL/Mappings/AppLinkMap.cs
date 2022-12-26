using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class AppLinkMap : EntityTypeConfiguration<AppLink>
    {
        public AppLinkMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(t => t.AppId)
                .HasColumnOrder(1);

            this.Property(t => t.Caption)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.Description)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.Url)
                .IsRequired()
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(x => x.DisplayOrder)
                .HasColumnOrder(5);

            this.Property(t => t.Guid)
                .HasColumnOrder(6)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true } 
                }));

            // Table & Column Mappings
            this.ToTable("AppLink");

            this.Property(t => t.Id)
                .HasColumnName("AppLinkId");

            // Relationships
            this.HasRequired(t => t.App)
                .WithMany(t => t.AppLinks)
                .HasForeignKey(d => d.AppId);
        }
    }
}
