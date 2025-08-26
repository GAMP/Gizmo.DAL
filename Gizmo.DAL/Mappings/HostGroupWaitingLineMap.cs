using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Host group waiting line map.
    /// </summary>
    public class HostGroupWaitingLineMap : IEntityTypeConfiguration<HostGroupWaitingLine>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<HostGroupWaitingLine> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .ValueGeneratedNever()
                .HasColumnName("HostGroupId")
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
