using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class PresetTimeSaleMap : IEntityTypeConfiguration<PresetTimeSale>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<PresetTimeSale> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("PresetTimeSaleId")
                .HasColumnOrder(0);

            builder.Property(x => x.Value)
                .HasColumnOrder(1);

            builder.Property(x => x.DisplayOrder)
                .HasColumnOrder(2);

            builder.ToTable(nameof(PresetTimeSale));
        }
    }
}
