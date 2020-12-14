using GizmoDALV2.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class PluginLibraryMap : EntityTypeConfiguration<PluginLibrary>
    {
        public PluginLibraryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("PluginLibraryId");

            this.Property(x => x.FileName)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_FileName") { IsUnique = true } 
                }));

            // Table & Column Mappings
            this.ToTable("PluginLibrary");
        }
    }
}
