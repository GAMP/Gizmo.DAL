using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class PresetTimeSaleMoneyMap : EntityTypeConfiguration<PresetTimeSaleMoney>
    {
        public PresetTimeSaleMoneyMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("PresetTimeSaleMoneyId")
                .HasColumnOrder(0);

            Property(x => x.Value)
                .HasColumnOrder(1);

            Property(x => x.DisplayOrder)
                .HasColumnOrder(2);

            ToTable(nameof(PresetTimeSaleMoney));
        }
    }
}
