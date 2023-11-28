using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AssistanceRequestTypeMap : EntityTypeConfiguration<Entities.AssistanceRequestType>
    {
        public AssistanceRequestTypeMap()
        {
            HasKey(t => t.Id);

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("AssistanceRequestTypeId");

            Property(x => x.Title)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired();

            Property(x => x.DisplayOrder)
                .HasColumnOrder(2);

            Property(x => x.IsDeleted)
                .HasColumnOrder(3);        

            ToTable(nameof(Entities.AssistanceRequestType));
        }
    }
}
