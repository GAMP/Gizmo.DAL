using GizmoDALV2.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    /// <summary>
    /// License key map.
    /// </summary>
    public class LicenseKeyMap : EntityTypeConfiguration<LicenseKey>
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        public LicenseKeyMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasColumnName("LicenseKeyId");

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(t => t.LicenseId)
                .HasColumnOrder(1);

            Property(x => x.Value)
                .HasColumnOrder(2)
                .HasMaxLength(SQLByteArraySize.NORMAL);

            Property(t => t.Comment)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(3);            

            Property(x => x.Guid)
                .HasColumnOrder(4)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true } 
                }));

            Property(x => x.IsEnabled)
                .HasColumnOrder(5);

            Property(x => x.AssignedHostId)
                .HasColumnOrder(6)
                .IsOptional();

            // Table & Column Mappings
            ToTable(nameof(LicenseKey));      

            // Relationships
            HasRequired(t => t.License)
                .WithMany(t => t.LicenseKeys)
                .HasForeignKey(d => d.LicenseId);

            HasOptional(x => x.AssignedHost)
                .WithMany()
                .HasForeignKey(x => x.AssignedHostId);
        }
    }
}
