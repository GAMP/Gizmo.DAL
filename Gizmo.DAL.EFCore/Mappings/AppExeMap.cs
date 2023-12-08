using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class AppExeMap : IEntityTypeConfiguration<AppExe>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppExe> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.AppId)
                .HasColumnOrder(1);

            builder.Property(t => t.Caption)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);
            
            builder.Property(t => t.Description)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.ExecutablePath)
                .HasColumnOrder(4)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.Arguments)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.WorkingDirectory)
                .HasColumnOrder(6)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.Modes)
                .HasColumnOrder(7);

            builder.Property(x => x.RunMode)
                .HasColumnOrder(8);

            builder.Property(x => x.DefaultDeploymentId)
                .HasColumnOrder(9);

            builder.Property(x => x.ReservationType)
               .HasColumnOrder(10);

            builder.Property(x => x.DisplayOrder)
                .HasColumnOrder(11);       

            builder.Property(x => x.Options)
                .HasColumnOrder(12);

            builder.Property(x => x.Guid)
                .HasColumnOrder(13);

            builder.Property(x => x.Accessible)
                .HasColumnOrder(14);

            builder.Property(t => t.Id).
                HasColumnName("AppExeId");

            // Table & Column Mappings
            builder.ToTable("AppExe");

            // Relationships
            builder.HasOne(t => t.App)
                .WithMany(t => t.AppExes)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.DefaultDeployment)
                .WithMany(t => t.DefaultedAppExes)
                .HasForeignKey(d => d.DefaultDeploymentId);
        }
    }
}
