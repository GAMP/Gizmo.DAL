using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Preset time sale money map.
    /// </summary>
    public class PresetTimeSaleMoneyMap : IEntityTypeConfiguration<PresetTimeSaleMoney>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PresetTimeSaleMoney> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("PresetTimeSaleMoneyId")
                .HasColumnOrder(0);

            builder.Property(x => x.Value)
                .HasColumnOrder(1);

            builder.Property(x => x.DisplayOrder)
                .HasColumnOrder(2);

            builder.ToTable(nameof(PresetTimeSaleMoney));
        }
    }
}
