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
    public class AppMap : EntityTypeConfiguration<App>
    {
        public AppMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasColumnName("AppId")
                .HasColumnOrder(0);

            this.Property(t => t.Title)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.PublisherId)
                .HasColumnOrder(2);

            this.Property(t => t.DeveloperId)
                .HasColumnOrder(3);

            this.Property(t => t.AppCategoryId)
                .HasColumnOrder(4);

            this.Property(t => t.Description)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.NORMAL);

            this.Property(t => t.ReleaseDate)
                .HasColumnOrder(6);

            this.Property(t => t.Version)
                .HasColumnOrder(7)
                .HasMaxLength(SQLStringSize.TINY45);

            this.Property(t => t.Options)
                .HasColumnOrder(8);

            this.Property(t => t.AgeRating)
                .HasColumnOrder(9);

            this.Property(t => t.Guid)
                .HasColumnOrder(10)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true } 
                }));

            this.Property(t => t.DefaultExecutableId)
                .HasColumnOrder(11);

            // Table & Column Mappings
            this.ToTable("App");

            // Relationships
            this.HasRequired(t => t.AppCategory)
                .WithMany(t => t.Apps)
                .HasForeignKey(d => d.AppCategoryId);

            this.HasOptional(t => t.Developer)
                .WithMany(t => t.DevelopedApps)
                .HasForeignKey(d => d.DeveloperId);

            this.HasOptional(t => t.Publisher)
                .WithMany(t => t.PublishedApps)
                .HasForeignKey(d => d.PublisherId);
        }
    }
}
