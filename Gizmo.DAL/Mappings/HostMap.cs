using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class HostMap : EntityTypeConfiguration<Host>
    {
        public HostMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("HostId")
                .HasColumnOrder(0);

            Property(x => x.Number)
                .HasColumnOrder(1);

            Property(x => x.Name)
                .IsRequired()
                .HasColumnOrder(2);

            Property(x => x.HostGroupId)
                .HasColumnOrder(3);

            Property(x => x.State)
                .HasColumnOrder(4);

            Property(x => x.IconId)
                .HasColumnOrder(5);

            Property(x => x.IsDeleted)
                .HasColumnOrder(6);

            Property(x => x.Guid)
                .HasColumnOrder(7)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true }
                }));

            // Table & Column Mappings
            ToTable(nameof(Host));

            // Relationships
            HasOptional(t => t.HostGroup)
                .WithMany(t => t.Hosts)
                .HasForeignKey(d => d.HostGroupId);

            HasOptional(x => x.Icon)
                .WithMany()
                .HasForeignKey(x => x.IconId);
        }
    }
}
