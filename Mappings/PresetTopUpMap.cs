using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Preset top up map.
    /// </summary>
    public sealed class PresetTopUpMap : IEntityTypeConfiguration<PresetTopUp>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<PresetTopUp> builder)
        {
            builder.ToTable(nameof(PresetTopUp));

            builder.HasKey(presetTopUp => presetTopUp.Id);

            builder.Property(presetTopUp => presetTopUp.Value)
                .HasColumnName("PresetTopUpId")
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(presetTopUp => presetTopUp.Value)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(presetTopUp => presetTopUp.DisplayOrder)
                .IsRequired()
                .HasColumnOrder(2);
        }
    }
}
