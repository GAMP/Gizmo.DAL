using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Preset reservation range map.
    /// </summary>
    public sealed class PresetReservationTimeMap : IEntityTypeConfiguration<PresetReservationTime>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PresetReservationTime> builder)
        {
            builder.ToTable(nameof(PresetReservationTime));

            builder.HasKey(presetReservationRange => presetReservationRange.Id);

            builder.Property(presetReservationRange => presetReservationRange.Id)
                .HasColumnName("PresetReservationTimeId")
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(presetReservationRange => presetReservationRange.Value)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(presetReservationRange => presetReservationRange.DisplayOrder)
                .IsRequired()
                .HasColumnOrder(2);
        }
    }
}
