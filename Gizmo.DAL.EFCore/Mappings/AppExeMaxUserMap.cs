using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class AppExeMaxUserMap : IEntityTypeConfiguration<AppExeMaxUser>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppExeMaxUser> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);          

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("AppExeMaxUserId")
                .HasColumnOrder(0);

            builder.Property(x => x.AppExeId)
                .HasColumnOrder(1); 

            builder.Property(x => x.Mode)
                .HasColumnOrder(2);

            builder.Property(x => x.MaxUsers)
                .HasColumnOrder(3);

            // Indexes
            builder.HasIndex(x => new { x.AppExeId, x.Mode }, "UQ_AppExeAppExeMode").IsUnique();
            
            // Table & Column Mappings
            builder.ToTable("AppExeMaxUser");

            // Relationships
            builder.HasOne(t => t.AppExe)
                .WithMany(t => t.MaxUsers)
                .HasForeignKey(d => d.AppExeId);
        }
    }
}
