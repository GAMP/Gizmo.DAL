using Gizmo.DAL.Entities;

using Gizmo.DAL;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AppExeCdImageMap : EntityTypeConfiguration<AppExeCdImage>
    {
        public AppExeCdImageMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.AppExeId)
                .HasColumnOrder(1);

            Property(t => t.Path)
                .IsRequired()
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.MountOptions)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            Property(x => x.DeviceId)
                .HasColumnOrder(4);

            Property(x => x.CheckExitCode)
                .HasColumnOrder(5);

            Property(t => t.Guid)
                .HasColumnOrder(6)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true }
                }));

            // Table & Column Mappings
            ToTable("AppExeCdImage");

            Property(t => t.Id)
                .HasColumnName("AppExeCdImageId");

            // Relationships
            HasRequired(t => t.AppExe)
                .WithMany(t => t.AppExeCdImages)
                .HasForeignKey(d => d.AppExeId);
        }
    }
}
