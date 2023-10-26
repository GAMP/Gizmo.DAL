using Gizmo.DAL.Entities;

using GizmoDALV2;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class HostComputerMap : EntityTypeConfiguration<HostComputer>
    {
        public HostComputerMap()
        {
            // Properties
            Property(x => x.Id);

            Property(t => t.Hostname)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.MACAddress)
                .IsRequired()
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_MACAddress") { IsUnique = true }
                }));

            // Table & Column Mappings
            ToTable("HostComputer");
        }
    }
}
