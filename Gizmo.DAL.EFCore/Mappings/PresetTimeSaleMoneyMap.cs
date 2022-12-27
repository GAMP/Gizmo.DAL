using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class PresetTimeSaleMoneyMap : IEntityTypeConfiguration<PresetTimeSaleMoney>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
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

            // Seeds
            builder.HasData(new PresetTimeSaleMoney() { Id = 1, Value = 1 });
            builder.HasData(new PresetTimeSaleMoney() { Id = 2, Value = 2 });
            builder.HasData(new PresetTimeSaleMoney() { Id = 3, Value = 5 });
            builder.HasData(new PresetTimeSaleMoney() { Id = 4, Value = 10 });
            builder.HasData(new PresetTimeSaleMoney() { Id = 5, Value = 20 });
        }
    }
}
