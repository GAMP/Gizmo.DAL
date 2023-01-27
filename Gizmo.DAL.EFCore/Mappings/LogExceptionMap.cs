using GizmoDALV2;
using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class LogExceptionMap : IEntityTypeConfiguration<LogException>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<LogException> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .ValueGeneratedNever();

            builder.Property(x => x.ExceptionData)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLByteArraySize.NORMAL);

            builder.Property(x => x.Id)
                .HasColumnName("LogId");

            // Indexes
            builder.HasIndex(t => t.Id);

            // Table & mappings
            builder.ToTable("LogException");

            builder.HasOne(x => x.Message)
                .WithOne(x => x.Exception)
                .HasForeignKey<LogException>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
