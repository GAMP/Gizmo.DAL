using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Schedule entity configuration.
    /// </summary>
    public sealed class ScheduleMap : IEntityTypeConfiguration<Schedule>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable(nameof(Schedule))
                .UseTptMappingStrategy();

            builder.HasKey(schedule => schedule.Id);

            builder.Property(schedule => schedule.Id)
                .HasColumnName("ScheduleId")
                .HasColumnOrder(0);

            builder.Property(schedule => schedule.Name)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnOrder(1);

            builder.Property(schedule => schedule.Description)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(2);

            builder.Property(schedule => schedule.Type)
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(schedule => schedule.StartTime)
                .IsRequired()
                .HasColumnOrder(4);

            builder.Property(schedule => schedule.IsDisabled)
                .IsRequired()
                .HasColumnOrder(5);

            builder.HasIndex(schedule => schedule.Name)
                .IsUnique()
                .HasFilter(null);
        }
    }
}
