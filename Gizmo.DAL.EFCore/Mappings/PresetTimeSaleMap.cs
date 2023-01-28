using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
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

            // Seeds
            builder.HasData(new PresetTimeSale() { Id = 1, Value = 1 });
            builder.HasData(new PresetTimeSale() { Id = 2, Value = 5 });
            builder.HasData(new PresetTimeSale() { Id = 3, Value = 15 });
            builder.HasData(new PresetTimeSale() { Id = 4, Value = 30 });
            builder.HasData(new PresetTimeSale() { Id = 5, Value = 60 });
        }
    }
}
