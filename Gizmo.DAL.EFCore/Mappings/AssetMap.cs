using GizmoDALV2;
using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class AssetMap : IEntityTypeConfiguration<Asset>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("AssetId")
                .HasColumnOrder(0);

            builder.Property(x => x.AssetTypeId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(t => t.Number)
                .HasColumnOrder(2);

            builder.Property(t => t.Tag)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(3);

            builder.Property(t => t.SmartCardUID)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(4);

            builder.Property(t => t.Barcode)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(5);

            builder.Property(t => t.SerialNumber)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(6);

            builder.Property(x => x.IsEnabled)
                .HasColumnOrder(7);

            // Indexes
            builder.HasIndex(t => t.SmartCardUID).HasDatabaseName("UQ_SmartCardUID").IsUnique();

            builder.HasIndex(t => t.Barcode).HasDatabaseName("UQ_Barcode").IsUnique();

            // Table & Column Mappings
            builder.ToTable(nameof(Asset));

            builder.HasOne(x=>x.AssetType)
                .WithMany(x => x.Assets)
                .HasForeignKey(x => x.AssetTypeId);

            builder.HasMany(x => x.Transactions)
                .WithOne(x => x.Asset)
                .HasForeignKey(x => x.AssetId);          
        }
    }
}
