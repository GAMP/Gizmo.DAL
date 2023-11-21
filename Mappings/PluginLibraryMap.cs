using Gizmo.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class PluginLibraryMap : EntityTypeConfiguration<PluginLibrary>
    {
        public PluginLibraryMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("PluginLibraryId");

            Property(x => x.FileName)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_FileName") { IsUnique = true } 
                }));

            // Table & Column Mappings
            ToTable("PluginLibrary");
        }
    }
}
