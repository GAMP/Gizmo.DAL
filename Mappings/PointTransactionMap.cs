using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class PointTransactionMap : IEntityTypeConfiguration<PointTransaction>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<PointTransaction> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("PointTransactionId");

            builder.Property(x => x.UserId)
                .HasColumnOrder(1);

            builder.Property(x => x.Type)
                .HasColumnOrder(2);

            builder.Property(x => x.Amount)
                .HasColumnOrder(3);

            builder.Property(x => x.Balance)
                .HasColumnOrder(4);

            builder.Property(x => x.IsVoided)
                .HasColumnOrder(5);

            // Table & Column Mappings
            builder.ToTable(nameof(PointTransaction));

            builder.HasOne(x => x.User)
                .WithMany(x => x.LoayalityPoints)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.ModifiedBy)
                .WithMany()
                .HasForeignKey(x => x.ModifiedById);
        }
    }
}
