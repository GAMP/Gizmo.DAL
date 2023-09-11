using GizmoDALV2;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AssistanceRequestMap : EntityTypeConfiguration<Entities.AssistanceRequest>
    {
        public AssistanceRequestMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("AssistanceRequestId");

            Property(x => x.Note)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired();


            // Table & Column Mappings
            ToTable(nameof(Entities.AssistanceRequest));
        }
    }
}
