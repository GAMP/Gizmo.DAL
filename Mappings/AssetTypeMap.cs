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
    public class AssetTypeMap : EntityTypeConfiguration<AssetType>
    {
        public AssetTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("AssetTypeId")
                .HasColumnOrder(0);

            this.Property(x => x.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            this.Property(t => t.Description)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.NORMAL);

            this.Property(t => t.PartNumber)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            // Table & Column Mappings
            this.ToTable(nameof(AssetType));

            this.HasMany(x => x.Assets)
                .WithRequired(x => x.AssetType)
                .HasForeignKey(x => x.AssetTypeId);
        }
    }
}
