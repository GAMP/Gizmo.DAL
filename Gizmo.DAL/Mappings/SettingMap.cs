using Gizmo.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    /// <summary>
    /// Settings entity map.
    /// </summary>
    public class SettingMap : EntityTypeConfiguration<Setting>
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        public SettingMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_NameGroup") { IsUnique = true , Order = 0 }
                }));

            Property(t => t.GroupName)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_NameGroup") { IsUnique = true , Order = 1 }
                })); ;

            Property(t => t.Value)
                .HasColumnOrder(3);

            // Table & Column Mappings
            ToTable("Setting");

            Property(x => x.Id)
                .HasColumnName("SettingId");
        }
    }
}
