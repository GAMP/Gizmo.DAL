using Gizmo.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class UserCreditLimitMap : EntityTypeConfiguration<UserCreditLimit>
    {
        public UserCreditLimitMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(x => x.CreditLimit);

            // Table & Column Mappings
            ToTable("UserCreditLimit");

            Property(t => t.Id).
                HasColumnName("UserId");

            // Relationships
            HasRequired(t => t.User)
                .WithRequiredDependent(t => t.UserCreditLimit)
                .WillCascadeOnDelete(true);
        }
    }
}
