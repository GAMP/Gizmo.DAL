using Gizmo.DAL.Entities;

using Gizmo.DAL;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class HostGroupMap : EntityTypeConfiguration<HostGroup>
    {
        public HostGroupMap()
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
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            Property(x => x.AppGroupId)
                .HasColumnOrder(2);

            Property(x => x.SecurityProfileId)
                .HasColumnOrder(3);

            Property(t => t.SkinName)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            Property(x => x.Options)
                .HasColumnOrder(5);

            Property(x => x.DefaultGuestGroupId)
                .HasColumnOrder(6)
                .IsOptional();

            // Table & Column Mappings
            ToTable(nameof(HostGroup));

            Property(t => t.Id)
                .HasColumnName("HostGroupId");

            // Relationships
            HasOptional(t => t.AppGroup)
                .WithMany(t => t.HostGroups)
                .HasForeignKey(d => d.AppGroupId);

            HasOptional(t => t.SecurityProfile)
                .WithMany(t => t.HostGroups)
                .HasForeignKey(d => d.SecurityProfileId);
        }
    }
}
