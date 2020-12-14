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
    public class HostMap : EntityTypeConfiguration<Host>
    {
        public HostMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("HostId")
                .HasColumnOrder(0);

            this.Property(x => x.Number)
                .HasColumnOrder(1);

            this.Property(x => x.Name)
                .IsRequired()
                .HasColumnOrder(2);

            this.Property(x => x.HostGroupId)
                .HasColumnOrder(3);

            this.Property(x => x.State)
                .HasColumnOrder(4);

            this.Property(x => x.IconId)
                .HasColumnOrder(5);

            this.Property(x => x.IsDeleted)
                .HasColumnOrder(6);

            // Table & Column Mappings
            this.ToTable(nameof(Host));

            // Relationships
            this.HasOptional(t => t.HostGroup)
                .WithMany(t => t.Hosts)
                .HasForeignKey(d => d.HostGroupId);

            this.HasOptional(x => x.Icon)
                .WithMany()
                .HasForeignKey(x => x.IconId);
        }
    }
}
