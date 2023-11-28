using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class AssetTransactionMap : IEntityTypeConfiguration<AssetTransaction>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AssetTransaction> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("AssetTransactionId")
                .HasColumnOrder(0);

            builder.Property(x => x.AssetTypeId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(x => x.AssetTypeName)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(x => x.AssetId)
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(x => x.IsActive)
                .HasColumnOrder(4);

            builder.Property(x => x.CheckedInById)
                .HasColumnOrder(5);

            builder.Property(x => x.CheckInTime)
                .HasColumnOrder(6);

            // Table & Column Mappings
            builder.ToTable(nameof(AssetTransaction));

            builder.HasOne(x => x.User)
                .WithMany(x => x.AssetTransactions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.AssetType)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.AssetTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Asset)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.AssetId);
        }
    }
}
