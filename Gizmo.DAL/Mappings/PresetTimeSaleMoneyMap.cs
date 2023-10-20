using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class PresetTimeSaleMoneyMap : EntityTypeConfiguration<PresetTimeSaleMoney>
    {
        public PresetTimeSaleMoneyMap()
        {
            this.HasKey(x => x.Id);

            this.Property(x => x.Id)
                .HasColumnName("PresetTimeSaleMoneyId")
                .HasColumnOrder(0);

            this.Property(x => x.Value)
                .HasColumnOrder(1);

            this.Property(x => x.DisplayOrder)
                .HasColumnOrder(2);

            this.ToTable(nameof(PresetTimeSaleMoney));
        }
    }
}
