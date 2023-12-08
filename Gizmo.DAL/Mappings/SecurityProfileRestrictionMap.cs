using Gizmo.DAL.Entities;

using Gizmo.DAL;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class SecurityProfileRestrictionMap : EntityTypeConfiguration<SecurityProfileRestriction>
    {
        public SecurityProfileRestrictionMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.SecurityProfileId)
                .HasColumnOrder(1);

            Property(t => t.Parameter)
                .IsRequired()
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            Property(x => x.Type)
                .HasColumnOrder(3);

            // Table & Column Mappings
            ToTable("SecurityProfileRestriction");

            Property(t => t.Id)
                .HasColumnName("SecurityProfileRestrictionId");

            // Relationships
            HasRequired(t => t.SecurityProfile)
                .WithMany(t => t.Restrictions)
                .HasForeignKey(d => d.SecurityProfileId);
        }
    }
}
