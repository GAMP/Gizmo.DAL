using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
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

            byte[] salt = new byte[] { 213, 217, 89, 164, 125, 194, 157, 170, 86, 35, 202, 5, 236, 165, 229, 151, 191, 209, 130, 41, 234, 120, 64, 104, 216, 200, 194, 9, 221, 163, 100, 236, 125, 143, 49, 114, 227, 161, 166, 20, 120, 7, 250, 81, 128, 236, 241, 116, 231, 235, 216, 208, 131, 155, 104, 218, 249, 75, 34, 190, 62, 160, 147, 82, 158, 78, 172, 74, 131, 17, 26, 236, 95, 7, 190, 245, 165, 235, 103, 17, 172, 55, 141, 182, 51, 96, 212, 209, 67, 164, 111, 234, 83, 101, 64, 224, 84, 84, 54, 4 };
            byte[] password = DefaultDbContext.GetHashedPassword("admin", salt);

            //byte[] password = new byte[] { 227, 190, 117, 189, 14, 131, 27, 251, 244, 196, 14, 55, 126, 183, 143, 152, 146, 122, 121, 195, 5, 57, 241, 24, 184, 41, 122, 231, 166, 174, 210, 155, 233, 8, 83, 128, 145, 208, 28, 139, 149, 46, 168, 246, 21, 38, 126, 197, 29, 147, 234, 81, 253, 218, 217, 136, 216, 237, 206, 196, 113, 231, 152, 52 };
            
            // Seeds
            builder.HasData(new UserCredential() { Id = 1, Salt = salt, Password = password });
        }
    }
}
