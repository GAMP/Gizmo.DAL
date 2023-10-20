using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class HostGroupUserBillProfileMap : EntityTypeConfiguration<HostGroupUserBillProfile>
    {
        public HostGroupUserBillProfileMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("HostGroupUserBillProfileId")
                .HasColumnOrder(0);

            this.Property(x => x.HostGroupId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_HostGroupUserBillProfile") { IsUnique = true, Order = 0 } })); ;

            this.Property(x => x.UserGroupId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_HostGroupUserBillProfile") { IsUnique = true, Order = 1 } })); ;

            this.Property(x => x.BillProfileId)
                .HasColumnOrder(3);

            this.Property(x => x.IsEnabled)
                .HasColumnOrder(4);

            this.HasRequired(x => x.HostGroup)
                .WithMany(x => x.UserBillProfiles)
                .HasForeignKey(x => x.HostGroupId)
                .WillCascadeOnDelete(true);

            this.HasRequired(x => x.BillProfile)
                .WithMany()
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.UserGroup)
                .WithMany()
                .WillCascadeOnDelete(false);

            // Table & Column Mappings
            this.ToTable(nameof(HostGroupUserBillProfile));
        }
    }
}
