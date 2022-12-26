using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class AssetMap : EntityTypeConfiguration<Asset>
    {
        public AssetMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("AssetId")
                .HasColumnOrder(0);

            this.Property(x => x.AssetTypeId)
                .IsRequired()
                .HasColumnOrder(1);

            this.Property(t => t.Number)
                .HasColumnOrder(2);

            this.Property(t => t.Tag)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(3);

            this.Property(t => t.SmartCardUID)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(4)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_SmartCardUID") { IsUnique = true }
                })); 

            this.Property(t => t.Barcode)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(5)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Barcode") { IsUnique = true }
                }));

            this.Property(t => t.SerialNumber)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(6);

            this.Property(x => x.IsEnabled)
                .HasColumnOrder(7);

            // Table & Column Mappings
            this.ToTable(nameof(Asset));

            this.HasRequired(x=>x.AssetType)
                .WithMany(x => x.Assets)
                .HasForeignKey(x => x.AssetTypeId);

            this.HasMany(x => x.Transactions)
                .WithRequired(x => x.Asset)
                .HasForeignKey(x => x.AssetId);          
        }
    }
}
