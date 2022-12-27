using GizmoDALV2;
using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class SecurityProfileRestrictionMap : IEntityTypeConfiguration<SecurityProfileRestriction>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<SecurityProfileRestriction> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.SecurityProfileId)
                .HasColumnOrder(1);

            builder.Property(t => t.Parameter)
                .IsRequired()
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.Type)
                .HasColumnOrder(3);

            // Table & Column Mappings
            builder.ToTable("SecurityProfileRestriction");

            builder.Property(t => t.Id)
                .HasColumnName("SecurityProfileRestrictionId");

            // Relationships
            builder.HasOne(t => t.SecurityProfile)
                .WithMany(t => t.Restrictions)
                .HasForeignKey(d => d.SecurityProfileId);
        }
    }
}
