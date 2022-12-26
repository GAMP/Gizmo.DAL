using GizmoDALV2.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class SecurityProfilePolicyMap : EntityTypeConfiguration<SecurityProfilePolicy>
    {
        public SecurityProfilePolicyMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.SecurityProfileId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_SecurityProfilePolicyType") { IsUnique = true, Order = 0 } }));

            Property(x => x.Type)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_SecurityProfilePolicyType") { IsUnique = true, Order = 1 } }));

            // Table & Column Mappings
            ToTable("SecurityProfilePolicy");

            Property(t => t.Id)
                .HasColumnName("SecurityProfilePolicyId");

            // Relationships
            HasRequired(t => t.SecurityProfile)
                .WithMany(t => t.Policies)
                .HasForeignKey(d => d.SecurityProfileId);
        }
    }
}
