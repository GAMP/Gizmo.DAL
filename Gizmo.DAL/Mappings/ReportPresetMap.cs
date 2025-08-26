using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// <see cref="ReportPreset"/> mapping.
    /// </summary>
    public sealed class ReportPresetMap : IEntityTypeConfiguration<ReportPreset>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Configure(EntityTypeBuilder<ReportPreset> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("ReportPresetId");

            builder.Property(x => x.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Report)
                .HasColumnOrder(2);

            builder.Property(x => x.Range)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(x=>x.Filters)               
                .HasColumnOrder(4);

            builder.Property(x=>x.DisplayOrder) 
                .IsRequired()
                .HasColumnOrder(5);

            builder.HasIndex(x=>x.Name).IsUnique();
            
            builder.ToTable(nameof(ReportPreset));
        }
    }
}
