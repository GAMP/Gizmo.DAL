using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class HostGroupUserBillProfileMap : EntityTypeConfiguration<HostGroupUserBillProfile>
    {
        public HostGroupUserBillProfileMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("HostGroupUserBillProfileId")
                .HasColumnOrder(0);

            Property(x => x.HostGroupId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_HostGroupUserBillProfile") { IsUnique = true, Order = 0 } })); ;

            Property(x => x.UserGroupId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_HostGroupUserBillProfile") { IsUnique = true, Order = 1 } })); ;

            Property(x => x.BillProfileId)
                .HasColumnOrder(3);

            Property(x => x.IsEnabled)
                .HasColumnOrder(4);

            HasRequired(x => x.HostGroup)
                .WithMany(x=>x.UserBillProfiles)
                .HasForeignKey(x=>x.HostGroupId)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.BillProfile)
                .WithMany()
                .WillCascadeOnDelete(false);

            HasRequired(x => x.UserGroup)
                .WithMany()
                .WillCascadeOnDelete(false);

            // Table & Column Mappings
            ToTable(nameof(HostGroupUserBillProfile));
        }
    }
}
