using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class PluginLibraryMap : IEntityTypeConfiguration<PluginLibrary>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<PluginLibrary> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("PluginLibraryId");

            // Indexes
            builder.HasIndex(t => t.FileName).HasDatabaseName("UQ_FileName").IsUnique();

            // Table & Column Mappings
            builder.ToTable("PluginLibrary");
        }
    }
}
