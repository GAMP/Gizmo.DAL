using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class UserAgreementMap : EntityTypeConfiguration<Entities.UserAgreement>
    {
        public UserAgreementMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("UserAgreementId");

            Property(x => x.Aggrement)
                .HasColumnOrder(1)
                .IsRequired();

            Property(x => x.Options)
                .HasColumnOrder(2);

            Property(x => x.DisplayOptions)
                .HasColumnOrder(3);

            Property(x => x.DisplayOrder)
                .HasColumnOrder(4);             


            // Table & Column Mappings
            ToTable(nameof(Entities.UserAgreement));
        }
    }
}
