using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class HostGroupWaitingLineMap : IEntityTypeConfiguration<HostGroupWaitingLine>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<HostGroupWaitingLine> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .ValueGeneratedNever()
                .HasColumnName("HosGroupId")
                .HasColumnOrder(0);

            builder.Property(entity => entity.TimeOutOptions)
                .HasColumnOrder(1);

            builder.Property(entity => entity.EnablePriorities)
                .HasColumnOrder(2);

            // Indexes
            builder.HasIndex(x => x.Id);

            builder.HasOne(entity => entity.HostGroup)
                .WithOne(hostGroup => hostGroup.WaitingLine)
                .HasForeignKey<HostGroupWaitingLine>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(nameof(HostGroupWaitingLine));
        }
    }
}
