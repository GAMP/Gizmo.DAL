using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class AppExeMap : EntityTypeConfiguration<AppExe>
    {
        public AppExeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(x => x.AppId)
                .HasColumnOrder(1);

            this.Property(t => t.Caption)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.Description)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.ExecutablePath)
                .HasColumnOrder(4)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.Arguments)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.WorkingDirectory)
                .HasColumnOrder(6)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(x => x.Modes)
                .HasColumnOrder(7);

            this.Property(x => x.RunMode)
                .HasColumnOrder(8);

            this.Property(x => x.DefaultDeploymentId)
                .HasColumnOrder(9);

            this.Property(x => x.ReservationType)
               .HasColumnOrder(10);

            this.Property(x => x.DisplayOrder)
                .HasColumnOrder(11);

            this.Property(x => x.Options)
                .HasColumnOrder(12);

            this.Property(x => x.Guid)
                .HasColumnOrder(13);

            this.Property(x => x.Accessible)
                .HasColumnOrder(14);

            // Table & Column Mappings
            this.ToTable("AppExe");

            this.Property(t => t.Id).
                HasColumnName("AppExeId");

            // Relationships
            this.HasRequired(t => t.App)
                .WithMany(t => t.AppExes)
                .HasForeignKey(d => d.AppId)
                .WillCascadeOnDelete(false);

            this.HasOptional(t => t.DefaultDeployment)
                .WithMany(t => t.DefaultedAppExes)
                .HasForeignKey(d => d.DefaultDeploymentId);
        }
    }
}
