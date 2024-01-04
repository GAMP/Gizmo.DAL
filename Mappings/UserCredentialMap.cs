using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class UserCredentialMap : IEntityTypeConfiguration<UserCredential>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserCredential> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .ValueGeneratedNever();

            builder.Property(x => x.Password)
                .HasColumnOrder(3)
                .HasMaxLength(64)
                .IsFixedLength();

            builder.Property(x => x.Salt)
                .HasColumnOrder(4)
                .HasMaxLength(100)
                .IsFixedLength();


            builder.Property(t => t.Id).
                HasColumnName("UserId");

            // Indexes
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("UserCredential");

            // Relationships
            builder.HasOne(t => t.User)
                .WithOne(t => t.UserCredential)
                .HasForeignKey<UserCredential>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
