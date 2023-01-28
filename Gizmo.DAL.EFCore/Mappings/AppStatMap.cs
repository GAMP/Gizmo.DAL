using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class AppStatMap : IEntityTypeConfiguration<AppStat>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppStat> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.AppId)
                .HasColumnOrder(1);

            builder.Property(x => x.AppExeId)
                .HasColumnOrder(2);

            builder.Property(x => x.HostId)
                .HasColumnOrder(3);

            builder.Property(x => x.UserId)
                .HasColumnOrder(4);

            builder.Property(x => x.Span)
                .HasColumnOrder(5);

            builder.Property(x => x.StartTime)
                .HasColumnOrder(6);

            builder.Property(t => t.Id)
                .HasColumnName("AppStatId");

            // Table & Column Mappings
            builder.ToTable("AppStat");

            // Indexes
            builder.HasIndex(x => x.AppExeId);

            // Relationships
            builder.HasOne(t => t.App)
                .WithMany(t => t.AppStats)
                .HasForeignKey(d => d.AppId);

            builder.HasOne(t => t.AppExe)
                .WithMany(t => t.AppStats)
                .HasForeignKey(d => d.AppExeId);

            builder.HasOne(t => t.Host)
                .WithMany(t => t.AppStats)
                .HasForeignKey(d => d.HostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.User)
                .WithMany(x => x.AppStats)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
