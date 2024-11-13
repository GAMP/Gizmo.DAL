using Gizmo.DAL.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Stock entity map.
    /// </summary>
    public sealed class StockMap : IEntityTypeConfiguration<Stock>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(stock => stock.Id);

            builder.Property(stock => stock.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(stock => stock.Type)
                .HasColumnOrder(2)
                .IsRequired();

            builder.HasIndex(stock => new { stock.Name, stock.BranchId }).IsUnique().HasFilter(null);
        }
    }
}
