using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// User entity map.
    /// </summary>
    public class UserMap : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User))
                .UseTptMappingStrategy();

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id)
                .HasColumnName("UserId");

            builder.Property(user => user.FirstName)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(user => user.LastName)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(user => user.BirthDate);

            builder.Property(user => user.Address)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(user => user.City)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(user => user.Country)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(user => user.PostCode)
                .HasMaxLength(20);

            builder.Property(user => user.Phone)
                .HasMaxLength(20);

            builder.Property(user => user.MobilePhone)
                .HasMaxLength(20);

            builder.Property(user => user.Sex);

            builder.Property(user => user.SmartCardUID)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(user => user.Identification)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(user => user.PreferredChannel)
                .IsRequired(false);

            builder.Property(user => user.PermissionSetId)
                .IsRequired(false);
            
            builder.Property(user => user.BranchId)
                .IsRequired(false);

            builder.Property(user => user.Guid);

            builder.HasIndex(user => user.Guid).IsUnique();
            builder.HasIndex(user => user.SmartCardUID).IsUnique();
            builder.HasIndex(user => user.Identification).IsUnique();

            builder.HasOne(user => user.CreatedBy)
                .WithMany()
                .HasForeignKey(user => user.CreatedById);

            builder.HasOne(user => user.ModifiedBy)
                .WithMany()
                .HasForeignKey(user => user.ModifiedById);

            builder.HasOne(user => user.Branch)
                .WithMany(user => user.Users)
                .HasForeignKey(user => user.BranchId);
        }
    }
}
