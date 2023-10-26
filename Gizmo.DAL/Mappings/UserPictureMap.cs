using Gizmo.DAL.Entities;

using GizmoDALV2;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class UserPictureMap : EntityTypeConfiguration<UserPicture>
    {
        public UserPictureMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("UserId")
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(x => x.Picture)
                .HasColumnOrder(1)
                .HasMaxLength(SQLByteArraySize.MEDIUM);

            // Table & mappings
            ToTable(nameof(UserPicture));

            // Relations
            HasRequired(x => x.User)
                .WithRequiredDependent(x => x.UserPicture)
                .WillCascadeOnDelete(true);
        }
    }
}
