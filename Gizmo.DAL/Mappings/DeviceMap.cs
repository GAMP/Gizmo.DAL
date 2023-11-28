using Gizmo.DAL.Entities;

using Gizmo.DAL;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class DeviceMap : EntityTypeConfiguration<Device>
    {
        public DeviceMap()
        {
            //primary key configuration
            HasKey(e => e.Id);

            //primary key column configuration
            Property(e => e.Id)
                .HasColumnOrder(0)
                .HasColumnName("DeviceId");

            //device name configuration
            Property(e => e.Name)
                .HasColumnOrder(1)
                .HasColumnName(nameof(Device.Name))
                .IsOptional()
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            //is enabled property
            Property(e => e.IsEnabled)
                .HasColumnOrder(2);

            //table name configuration
            ToTable(nameof(Device));
        }
    }
}
