using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AppMap : EntityTypeConfiguration<App>
    {
        public AppMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("AppId")
                .HasColumnOrder(0);

            Property(t => t.Title)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.PublisherId)
                .HasColumnOrder(2);

            Property(t => t.DeveloperId)
                .HasColumnOrder(3);

            Property(t => t.AppCategoryId)
                .HasColumnOrder(4);

            Property(t => t.Description)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.NORMAL);

            Property(t => t.ReleaseDate)
                .HasColumnOrder(6);

            Property(t => t.Version)
                .HasColumnOrder(7)
                .HasMaxLength(SQLStringSize.TINY45);

            Property(t => t.Options)
                .HasColumnOrder(8);

            Property(t => t.AgeRating)
                .HasColumnOrder(9);

            Property(t => t.Guid)
                .HasColumnOrder(10)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true } 
                }));

            Property(t => t.DefaultExecutableId)
                .HasColumnOrder(11);

            // Table & Column Mappings
            ToTable("App");

            // Relationships
            HasRequired(t => t.AppCategory)
                .WithMany(t => t.Apps)
                .HasForeignKey(d => d.AppCategoryId);

            HasOptional(t => t.Developer)
                .WithMany(t => t.DevelopedApps)
                .HasForeignKey(d => d.DeveloperId);

            HasOptional(t => t.Publisher)
                .WithMany(t => t.PublishedApps)
                .HasForeignKey(d => d.PublisherId);
        }
    }
}
