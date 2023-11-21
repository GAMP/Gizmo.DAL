using Gizmo.DAL.Entities;

using GizmoDALV2;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AssetTransactionMap : EntityTypeConfiguration<AssetTransaction>
    {
        public AssetTransactionMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("AssetTransactionId")
                .HasColumnOrder(0);

            Property(x => x.AssetTypeId)
                .IsRequired()
                .HasColumnOrder(1);

            Property(x => x.AssetTypeName)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired()
                .HasColumnOrder(2);

            Property(x => x.AssetId)
                .IsRequired()
                .HasColumnOrder(3);

            Property(x => x.IsActive)
                .HasColumnOrder(4);

            Property(x => x.CheckedInById)
                .HasColumnOrder(5);

            Property(x => x.CheckInTime)
                .HasColumnOrder(6);

            // Table & Column Mappings
            ToTable(nameof(AssetTransaction));

            HasRequired(x=>x.User)
                .WithMany(x => x.AssetTransactions)
                .HasForeignKey(x => x.UserId);

            HasRequired(x => x.AssetType)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.AssetTypeId)
                .WillCascadeOnDelete(false);

            HasRequired(x=>x.Asset)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.AssetId);        
        }
    }
}
