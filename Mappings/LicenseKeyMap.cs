using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class LicenseKeyMap : EntityTypeConfiguration<LicenseKey>
    {
        public LicenseKeyMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(t => t.LicenseId)
                .HasColumnOrder(1);

            this.Property(x => x.Value)
                .HasColumnOrder(2)
                .HasMaxLength(SQLByteArraySize.NORMAL);

            this.Property(t => t.Comment)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(3);            

            this.Property(x => x.Guid)
                .HasColumnOrder(4)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true } 
                }));

            this.Property(x => x.IsEnabled)
                .HasColumnOrder(5);

            // Table & Column Mappings
            this.ToTable("LicenseKey");

            this.Property(t => t.Id)
                .HasColumnName("LicenseKeyId");

            // Relationships
            this.HasRequired(t => t.License)
                .WithMany(t => t.LicenseKeys)
                .HasForeignKey(d => d.LicenseId);
        }
    }
}
