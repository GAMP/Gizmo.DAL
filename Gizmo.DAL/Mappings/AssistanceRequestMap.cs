using GizmoDALV2;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AssistanceRequestMap : EntityTypeConfiguration<Entities.AssistanceRequest>
    {
        public AssistanceRequestMap()
        {
            HasKey(t => t.Id);

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("AssistanceRequestId");

            Property(x => x.UserId)
                .IsOptional()
                .HasColumnOrder(1);

            Property(x => x.HostId)
                .IsRequired()
                .HasColumnOrder(2);

            Property(x => x.AssistanceRequestTypeId)
                .IsRequired()
                .HasColumnOrder(3);

            Property(x => x.Note)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY)
                .IsOptional();

            Property(x => x.Status)
                .HasColumnOrder(5);

            HasRequired(x => x.AssistanceRequestType)
                .WithMany(x => x.AssistanceRequests)
                .HasForeignKey(x => x.AssistanceRequestTypeId);

            HasRequired(x => x.Host)
                .WithMany(x => x.AssitanceRequests)
                .HasForeignKey(x => x.HostId);

            ToTable(nameof(Entities.AssistanceRequest));
        }
    }
}
