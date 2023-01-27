using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
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
