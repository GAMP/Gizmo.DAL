using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class NoteMap : EntityTypeConfiguration<Note>
    {
        public NoteMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("NoteId")
                .HasColumnOrder(0);

            Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(SQLStringSize.MEDIUM);

            Property(x => x.Options)
                .HasColumnOrder(1);

            Property(x => x.Sevirity)
                .HasColumnOrder(2);

            Property(x => x.IsDeleted)
                .HasColumnOrder(3);

            // Table & Column Mappings
            ToTable(nameof(Note));
        }
    }

    public class UserNoteMap : EntityTypeConfiguration<UserNote>
    {
        public UserNoteMap()
        {
            HasRequired(x => x.User)
                .WithMany(x => x.Notes)
                .HasForeignKey(x => x.UserId);

            ToTable(nameof(UserNote));
        }
    }
}
