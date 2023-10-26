using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class HostLayoutGroupLayoutMap : EntityTypeConfiguration<HostLayoutGroupLayout>
    {
        public HostLayoutGroupLayoutMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("HostLayoutGroupLayoutId")
                .HasColumnOrder(0);

            Property(x => x.HostLayoutGroupId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_HostLayoutGroupHost") { IsUnique = true, Order = 0 } }));

            Property(x => x.HostId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_HostLayoutGroupHost") { IsUnique = true, Order = 1 } }));

            Property(x => x.X)
                .HasColumnOrder(3);

            Property(x => x.Y)
                .HasColumnOrder(4);

            Property(x => x.Height)
                .HasColumnOrder(5);

            Property(x => x.Width)
                .HasColumnOrder(6);

            Property(x => x.IsHidden)
                .HasColumnOrder(7);

            // Table & Column Mappings
            ToTable("HostLayoutGroupLayout");

            HasRequired(x => x.HostLayoutGroup)
                .WithMany(x => x.Layouts)
                .HasForeignKey(x => x.HostLayoutGroupId);

            HasRequired(x => x.Host)
                .WithMany(x => x.Layouts)
                .HasForeignKey(x => x.HostId);
        }
    }
}
