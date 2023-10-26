using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AppExeMaxUserMap : EntityTypeConfiguration<AppExeMaxUser>
    {
        public AppExeMaxUserMap()
        {
            // Primary Key
            HasKey(x => x.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("AppExeMaxUserId")
                .HasColumnOrder(0);

            Property(x => x.AppExeId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_AppExeAppExeMode") { IsUnique = true, Order = 0 } }));

            Property(x => x.Mode)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_AppExeAppExeMode") { IsUnique = true, Order = 1 } }));

            Property(x => x.MaxUsers)
                .HasColumnOrder(3);

            // Table & Column Mappings
            ToTable("AppExeMaxUser");

            // Relationships
            HasRequired(t => t.AppExe)
                .WithMany(t => t.MaxUsers)
                .HasForeignKey(d => d.AppExeId);
        }
    }
}
