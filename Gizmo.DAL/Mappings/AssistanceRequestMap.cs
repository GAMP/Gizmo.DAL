using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Assistance request entity mapping.
    /// </summary>
    public class AssistanceRequestMap : IEntityTypeConfiguration<AssistanceRequest>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AssistanceRequest> builder)
        {
            builder.HasKey(assistanceRequest => assistanceRequest.Id);

            builder.Property(assistanceRequest => assistanceRequest.Id)
                .HasColumnOrder(0)
                .HasColumnName("AssistanceRequestId");

            builder.Property(assistanceRequest => assistanceRequest.UserId)
                .IsRequired(false)
                .HasColumnOrder(1);

            builder.Property(assistanceRequest => assistanceRequest.HostId)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(assistanceRequest => assistanceRequest.AssistanceRequestTypeId)
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(assistanceRequest => assistanceRequest.Note)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired(false);

            builder.Property(assistanceRequest => assistanceRequest.Status)
                .HasColumnOrder(5);

            builder.HasOne(assistanceRequest => assistanceRequest.AssistanceRequestType)
                .WithMany(x => x.AssistanceRequests)
                .HasForeignKey(x => x.AssistanceRequestTypeId);

            builder.HasOne(assistanceRequest => assistanceRequest.Host)
                .WithMany(host => host.AssistanceRequests)
                .HasForeignKey(assistanceRequest => assistanceRequest.HostId);

            builder.ToTable(nameof(AssistanceRequest));
        }
    }
}
