using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class PointTransactionMap : EntityTypeConfiguration<PointTransaction>
    {
        public PointTransactionMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("PointTransactionId");

            Property(x => x.UserId)
                .HasColumnOrder(1);

            Property(x => x.Type)
                .HasColumnOrder(2);

            Property(x => x.Amount)
                .HasColumnOrder(3);

            Property(x => x.Balance)
                .HasColumnOrder(4);

            Property(x => x.IsVoided)
                .HasColumnOrder(5);

            // Table & Column Mappings
            ToTable(nameof(PointTransaction));

            HasRequired(x => x.User)
                .WithMany(x => x.LoayalityPoints)
                .HasForeignKey(x => x.UserId);

            HasOptional(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById);

            HasOptional(x => x.ModifiedBy)
                .WithMany()
                .HasForeignKey(x => x.ModifiedById);
        }
    }
}
