using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GizmoDALV2;

namespace Gizmo.DAL.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("UserId");

            builder.Property(t => t.FirstName)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(t => t.LastName)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.BirthDate);

            builder.Property(t => t.Address)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.City)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(t => t.Country)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(t => t.PostCode)
                .HasMaxLength(20);

            builder.Property(t => t.Phone)
                .HasMaxLength(20);

            builder.Property(t => t.MobilePhone)
                .HasMaxLength(20);

            builder.Property(x => x.Sex);

            builder.Property(x => x.SmartCardUID)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.Identification)
                .HasMaxLength(SQLStringSize.TINY);

            // Indexes
            builder.HasIndex(t => t.Guid).HasDatabaseName("UQ_Guid").IsUnique();
            builder.HasIndex(t => t.SmartCardUID).HasDatabaseName("UQ_SmartCardUID").IsUnique();
            builder.HasIndex(t => t.Identification).HasDatabaseName("UQ_Identification").IsUnique();

            // Relations
            builder.HasOne(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById);

            // Table & Column Mappings
            builder.ToTable("User");
        }
    }
}
