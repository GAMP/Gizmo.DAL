using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class NoteMap : EntityTypeConfiguration<Note>
    {
        public NoteMap()
        {
            this.HasKey(x => x.Id);

            this.Property(x => x.Id)
                .HasColumnName("NoteId")
                .HasColumnOrder(0);

            this.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(SQLStringSize.MEDIUM);

            this.Property(x => x.Options)
                .HasColumnOrder(1);

            this.Property(x => x.Sevirity)
                .HasColumnOrder(2);

            this.Property(x => x.IsDeleted)
                .HasColumnOrder(3);

            // Table & Column Mappings
            this.ToTable(nameof(Note));
        }
    }

    public class UserNoteMap : EntityTypeConfiguration<UserNote>
    {
        public UserNoteMap()
        {
            this.HasRequired(x => x.User)
                .WithMany(x => x.Notes)
                .HasForeignKey(x => x.UserId);

            this.ToTable(nameof(UserNote));
        }
    }
}
