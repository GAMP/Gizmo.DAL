using GizmoDALV2;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AssistanceRequestTypeMap : EntityTypeConfiguration<Entities.AssistanceRequestType>
    {
        public AssistanceRequestTypeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("AssistanceRequestTypeId");

            Property(x => x.Title)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired();

        

            // Table & Column Mappings
            ToTable(nameof(Entities.AssistanceRequestType));
        }
    }
}
