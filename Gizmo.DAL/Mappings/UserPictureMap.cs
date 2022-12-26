using GizmoDALV2;
using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class UserPictureMap : EntityTypeConfiguration<UserPicture>
    {
        public UserPictureMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("UserId")
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(x => x.Picture)
                .HasColumnOrder(1)
                .HasMaxLength(SQLByteArraySize.MEDIUM);

            // Table & mappings
            this.ToTable(nameof(UserPicture));              

            // Relations
            this.HasRequired(x => x.User)
                .WithRequiredDependent(x => x.UserPicture)
                .WillCascadeOnDelete(true);
        }
    }
}
