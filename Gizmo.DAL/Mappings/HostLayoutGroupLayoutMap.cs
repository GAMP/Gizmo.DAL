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
    public class HostLayoutGroupLayoutMap : EntityTypeConfiguration<HostLayoutGroupLayout>
    {
        public HostLayoutGroupLayoutMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("HostLayoutGroupLayoutId")
                .HasColumnOrder(0);

            this.Property(x => x.HostLayoutGroupId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_HostLayoutGroupHost") { IsUnique = true, Order = 0 } }));

            this.Property(x => x.HostId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_HostLayoutGroupHost") { IsUnique = true, Order = 1 } }));

            this.Property(x => x.X)
                .HasColumnOrder(3);

            this.Property(x => x.Y)
                .HasColumnOrder(4);

            this.Property(x => x.Height)
                .HasColumnOrder(5);

            this.Property(x => x.Width)
                .HasColumnOrder(6);

            this.Property(x => x.IsHidden)
                .HasColumnOrder(7);

            // Table & Column Mappings
            this.ToTable("HostLayoutGroupLayout");

            this.HasRequired(x => x.HostLayoutGroup)
                .WithMany(x => x.Layouts)
                .HasForeignKey(x => x.HostLayoutGroupId);

            this.HasRequired(x => x.Host)
                .WithMany(x => x.Layouts)
                .HasForeignKey(x => x.HostId);
        }
    }
}
