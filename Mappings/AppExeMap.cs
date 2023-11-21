using Gizmo.DAL.Entities;

using GizmoDALV2;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AppExeMap : EntityTypeConfiguration<AppExe>
    {
        public AppExeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.AppId)
                .HasColumnOrder(1);

            Property(t => t.Caption)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);
            
            Property(t => t.Description)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.ExecutablePath)
                .HasColumnOrder(4)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.Arguments)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.WorkingDirectory)
                .HasColumnOrder(6)
                .HasMaxLength(SQLStringSize.TINY);

            Property(x => x.Modes)
                .HasColumnOrder(7);

            Property(x => x.RunMode)
                .HasColumnOrder(8);

            Property(x => x.DefaultDeploymentId)
                .HasColumnOrder(9);

            Property(x => x.ReservationType)
               .HasColumnOrder(10);

            Property(x => x.DisplayOrder)
                .HasColumnOrder(11);       

            Property(x => x.Options)
                .HasColumnOrder(12);

            Property(x => x.Guid)
                .HasColumnOrder(13);

            Property(x => x.Accessible)
                .HasColumnOrder(14);

            // Table & Column Mappings
            ToTable("AppExe");

            Property(t => t.Id).
                HasColumnName("AppExeId");

            // Relationships
            HasRequired(t => t.App)
                .WithMany(t => t.AppExes)
                .HasForeignKey(d => d.AppId)
                .WillCascadeOnDelete(false);

            HasOptional(t => t.DefaultDeployment)
                .WithMany(t => t.DefaultedAppExes)
                .HasForeignKey(d => d.DefaultDeploymentId);
        }
    }
}
