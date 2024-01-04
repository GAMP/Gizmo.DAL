using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class AssistanceRequestMap : IEntityTypeConfiguration<AssistanceRequest>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AssistanceRequest> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("AssistanceRequestId");

            builder.Property(x => x.UserId)
                .IsRequired(false)
                .HasColumnOrder(1);

            builder.Property(x => x.HostId)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(x => x.AssistanceRequestTypeId)
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(x => x.Note)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired(false);

            builder.Property(x => x.Status)
                .HasColumnOrder(5);

            builder.HasOne(x => x.AssistanceRequestType)
                .WithMany(x => x.AssistanceRequests)
                .HasForeignKey(x => x.AssistanceRequestTypeId);

            builder.HasOne(x => x.Host)
                .WithMany(x => x.AssitanceRequests)
                .HasForeignKey(x => x.HostId);

            builder.ToTable(nameof(AssistanceRequest));
        }
    }
}
