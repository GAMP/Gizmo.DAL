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

            Property(x => x.Name)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired();

            Property(x => x.Agreement)
                .HasColumnOrder(2)
                .IsRequired();

            Property(x => x.Options)
                .HasColumnOrder(3);

            Property(x => x.DisplayOptions)
                .HasColumnOrder(4);

            Property(x => x.DisplayOrder)
                .HasColumnOrder(5);

            Property(x => x.IsEnabled)
                .HasColumnOrder(6);

            // Table & Column Mappings
            ToTable(nameof(Entities.UserAgreement));
        }
    }
}
