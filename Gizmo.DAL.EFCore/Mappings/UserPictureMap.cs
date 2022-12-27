using GizmoDALV2;
using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class UserPictureMap : IEntityTypeConfiguration<UserPicture>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserPicture> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("UserId")
                .HasColumnOrder(0)
                .ValueGeneratedNever();

            builder.Property(x => x.Picture)
                .HasColumnOrder(1)
                .HasMaxLength(SQLByteArraySize.MEDIUM);

            // Indexes
            builder.HasIndex(t => t.Id);

            // Table & mappings
            builder.ToTable(nameof(UserPicture));              

            // Relations
            builder.HasOne(x => x.User)
                .WithOne(x => x.UserPicture)
                .HasForeignKey<UserPicture>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById);
        }
    }
}
