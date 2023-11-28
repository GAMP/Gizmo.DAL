using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class LogMap : IEntityTypeConfiguration<Log>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.Time)
                .HasColumnOrder(1);;

            builder.Property(x => x.HostNumber)
                .HasColumnOrder(2);

            builder.Property(t => t.Hostname)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.ModuleType)
                .HasColumnOrder(4);

            builder.Property(t => t.ModuleVersion)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Category)
                .HasColumnOrder(6);

            builder.Property(x => x.MessageType)
                .HasColumnOrder(7);

            builder.Property(t => t.Message)
                .IsRequired()
                .HasColumnOrder(8)
                .HasMaxLength(SQLStringSize.NORMAL);

           // Indexes
            builder.HasIndex(t => t.Time, "IX_Time");
            builder.HasIndex(t => t.HostNumber, "IX_HostNumber");
            builder.HasIndex(t => t.Category, "IX_Category");
            builder.HasIndex(t => t.MessageType, "IX_MessageType");

            // Table & Column Mappings
            builder.ToTable("Log");

            builder.Property(t => t.Id)
                .HasColumnName("LogId");
        }
    }
}
