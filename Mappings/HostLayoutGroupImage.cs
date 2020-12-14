using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class HostLayoutGroupImageMap : EntityTypeConfiguration<HostLayoutGroupImage>
    {
        public HostLayoutGroupImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(x => x.Image)
                .HasMaxLength(GizmoDALV2.SQLByteArraySize.MEDIUM);

            // Table & Column Mappings
            this.ToTable("HostLayoutGroupImage");

            this.Property(x => x.Id)
                .HasColumnName("HostLayoutGroupId");

            this.HasRequired(x => x.HostLayoutGroup)
                .WithRequiredDependent(x => x.Image)
                .WillCascadeOnDelete(true);
        }
    }
}
