using Gizmo.DAL.Entities;

using GizmoDALV2;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class AssistanceRequestTypeMap : IEntityTypeConfiguration<AssistanceRequestType>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AssistanceRequestType> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("AssistanceRequestTypeId");

            builder.Property(x => x.Title)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired();

            builder.Property(x => x.DisplayOrder)
                .HasColumnOrder(2);

            builder.Property(x => x.IsDeleted)
                .HasColumnOrder(3);

            builder.ToTable(nameof(AssistanceRequestType));
        }
    }
}
