using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

namespace GizmoDALV2.Mappings
{
    public class MonetaryUnitMap : IEntityTypeConfiguration<MonetaryUnit>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<MonetaryUnit> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("MonetaryUnitId")
                .HasColumnOrder(0);

            builder.Property(x => x.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Value)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .HasColumnOrder(3);

            // Indexes
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();

            // Table & Coulmn
            builder.ToTable(nameof(MonetaryUnit));

            // Seeds
            if (RegionInfo.CurrentRegion.ISOCurrencySymbol == "EUR")
            {
                builder.HasData(new MonetaryUnit() { Id = 1, Name = "1 Cent", Value = 0.01M, DisplayOrder = 0 });
                builder.HasData(new MonetaryUnit() { Id = 2, Name = "5 Cent", Value = 0.05M, DisplayOrder = 1 });
                builder.HasData(new MonetaryUnit() { Id = 3, Name = "10 Cent", Value = 0.10M, DisplayOrder = 2 });
                builder.HasData(new MonetaryUnit() { Id = 4, Name = "20 Cent", Value = 0.20M, DisplayOrder = 3 });
                builder.HasData(new MonetaryUnit() { Id = 5, Name = "50 Cent", Value = 0.50M, DisplayOrder = 4 });
                builder.HasData(new MonetaryUnit() { Id = 6, Name = "1 Euro", Value = 1.00M, DisplayOrder = 5 });
                builder.HasData(new MonetaryUnit() { Id = 7, Name = "2 Euro", Value = 2.00M, DisplayOrder = 6 });
                builder.HasData(new MonetaryUnit() { Id = 8, Name = "5 Euro", Value = 5.00M, DisplayOrder = 7 });
                builder.HasData(new MonetaryUnit() { Id = 9, Name = "10 Euro", Value = 10.00M, DisplayOrder = 8 });
                builder.HasData(new MonetaryUnit() { Id = 10, Name = "20 Euro", Value = 20.00M, DisplayOrder = 9 });
                builder.HasData(new MonetaryUnit() { Id = 11, Name = "50 Euro", Value = 50.00M, DisplayOrder = 10 });
                builder.HasData(new MonetaryUnit() { Id = 12, Name = "100 Euro", Value = 100.00M, DisplayOrder = 11 });
                builder.HasData(new MonetaryUnit() { Id = 13, Name = "200 Euro", Value = 200.00M, DisplayOrder = 12 });
                builder.HasData(new MonetaryUnit() { Id = 14, Name = "500 Euro", Value = 500.00M, DisplayOrder = 13 });
            }
            else
            {
                builder.HasData(new MonetaryUnit() { Id = 1, Name = "1 Cent", Value = 0.01M, DisplayOrder = 0 });
                builder.HasData(new MonetaryUnit() { Id = 2, Name = "5 Cent", Value = 0.05M, DisplayOrder = 1 });
                builder.HasData(new MonetaryUnit() { Id = 3, Name = "10 Cent", Value = 0.10M, DisplayOrder = 2 });
                builder.HasData(new MonetaryUnit() { Id = 4, Name = "25 Cent", Value = 0.25M, DisplayOrder = 3 });
                builder.HasData(new MonetaryUnit() { Id = 5, Name = "1 Dollar", Value = 1.00M, DisplayOrder = 4 });
                builder.HasData(new MonetaryUnit() { Id = 6, Name = "2 Dollar", Value = 2.00M, DisplayOrder = 5 });
                builder.HasData(new MonetaryUnit() { Id = 7, Name = "5 Dollar", Value = 5.00M, DisplayOrder = 6 });
                builder.HasData(new MonetaryUnit() { Id = 8, Name = "10 Dollar", Value = 10.00M, DisplayOrder = 7 });
                builder.HasData(new MonetaryUnit() { Id = 9, Name = "20 Dollar", Value = 20.00M, DisplayOrder = 8 });
                builder.HasData(new MonetaryUnit() { Id = 10, Name = "50 Dollar", Value = 50.00M, DisplayOrder = 9 });
                builder.HasData(new MonetaryUnit() { Id = 11, Name = "100 Dollar", Value = 100.00M, DisplayOrder = 10 });
            }
        }
    }
}
