using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AssetTypeMap : EntityTypeConfiguration<AssetType>
    {
        public AssetTypeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("AssetTypeId")
                .HasColumnOrder(0);

            Property(x => x.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            Property(t => t.Description)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.NORMAL);

            Property(t => t.PartNumber)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            // Table & Column Mappings
            ToTable(nameof(AssetType));

            HasMany(x => x.Assets)
                .WithRequired(x => x.AssetType)
                .HasForeignKey(x => x.AssetTypeId);
        }
    }
}
