using Gizmo.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class UserCredentialMap : EntityTypeConfiguration<UserCredential>
    {
        public UserCredentialMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(x => x.Password)
                .HasColumnOrder(3)
                .HasMaxLength(64)
                .IsFixedLength();

            Property(x => x.Salt)
                .HasColumnOrder(4)
                .HasMaxLength(100)
                .IsFixedLength();

            // Table & Column Mappings
            ToTable("UserCredential");

            Property(t => t.Id).
                HasColumnName("UserId");

            // Relationships
            HasRequired(t => t.User)
                .WithRequiredDependent(t => t.UserCredential)
                .WillCascadeOnDelete(true);
        }
    }
}
