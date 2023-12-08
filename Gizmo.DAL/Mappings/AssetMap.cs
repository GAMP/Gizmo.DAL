using Gizmo.DAL.Entities;

using Gizmo.DAL;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AssetMap : EntityTypeConfiguration<Asset>
    {
        public AssetMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("AssetId")
                .HasColumnOrder(0);

            Property(x => x.AssetTypeId)
                .IsRequired()
                .HasColumnOrder(1);

            Property(t => t.Number)
                .HasColumnOrder(2);

            Property(t => t.Tag)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(3);

            Property(t => t.SmartCardUID)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(4)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_SmartCardUID") { IsUnique = true }
                }));

            Property(t => t.Barcode)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(5)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Barcode") { IsUnique = true }
                }));

            Property(t => t.SerialNumber)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(6);

            Property(x => x.IsEnabled)
                .HasColumnOrder(7);

            // Table & Column Mappings
            ToTable(nameof(Asset));

            HasRequired(x => x.AssetType)
                .WithMany(x => x.Assets)
                .HasForeignKey(x => x.AssetTypeId);

            HasMany(x => x.Transactions)
                .WithRequired(x => x.Asset)
                .HasForeignKey(x => x.AssetId);
        }
    }
}
