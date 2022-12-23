using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class NoteMap : IEntityTypeConfiguration<Note>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("NoteId")
                .HasColumnOrder(0);

            builder.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(SQLStringSize.MEDIUM);

            builder.Property(x => x.Options)
                .HasColumnOrder(1);

            builder.Property(x => x.Sevirity)
                .HasColumnOrder(2);

            builder.Property(x => x.IsDeleted)
                .HasColumnOrder(3);

            // Table & Column Mappings
            builder.ToTable(nameof(Note));
        }
    }

    public class UserNoteMap : IEntityTypeConfiguration<UserNote>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserNote> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.Notes)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.ToTable(nameof(UserNote));
        }
    }
}
