using Gizmo.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class DepositIntentMap : EntityTypeConfiguration<DepositIntent>
    {
        public DepositIntentMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("DepositIntentId")
                .HasColumnOrder(0);

            Property(x => x.UserId)
                .IsRequired()
                .HasColumnOrder(1);

            Property(x => x.Amount)
                .HasColumnOrder(2);

            HasRequired(x => x.User)
                .WithMany(x => x.DepositIntents)
                .HasForeignKey(x => x.UserId);

            //Table
            ToTable(nameof(DepositIntent));
        }
    }
}
