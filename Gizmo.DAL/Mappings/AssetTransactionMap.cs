using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class AssetTransactionMap : EntityTypeConfiguration<AssetTransaction>
    {
        public AssetTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("AssetTransactionId")
                .HasColumnOrder(0);

            this.Property(x => x.AssetTypeId)
                .IsRequired()
                .HasColumnOrder(1);

            this.Property(x => x.AssetTypeName)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired()
                .HasColumnOrder(2);

            this.Property(x => x.AssetId)
                .IsRequired()
                .HasColumnOrder(3);

            this.Property(x => x.IsActive)
                .HasColumnOrder(4);

            this.Property(x => x.CheckedInById)
                .HasColumnOrder(5);

            this.Property(x => x.CheckInTime)
                .HasColumnOrder(6);

            // Table & Column Mappings
            this.ToTable(nameof(AssetTransaction));

            this.HasRequired(x => x.User)
                .WithMany(x => x.AssetTransactions)
                .HasForeignKey(x => x.UserId);

            this.HasRequired(x => x.AssetType)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.AssetTypeId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Asset)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.AssetId);
        }
    }
}
