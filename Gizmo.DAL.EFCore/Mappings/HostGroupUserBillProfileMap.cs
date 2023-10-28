using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class HostGroupUserBillProfileMap : IEntityTypeConfiguration<HostGroupUserBillProfile>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<HostGroupUserBillProfile> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("HostGroupUserBillProfileId")
                .HasColumnOrder(0);

            builder.Property(x => x.BillProfileId)
                .HasColumnOrder(3);

            builder.Property(x => x.IsEnabled)
                .HasColumnOrder(4);

            builder.HasOne(x => x.HostGroup)
                .WithMany(x=>x.UserBillProfiles)
                .HasForeignKey(x=>x.HostGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.BillProfile)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UserGroup)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(x => new { x.HostGroupId, x.UserGroupId }).HasDatabaseName("UQ_HostGroupUserBillProfile").IsUnique();

            // Table & Column Mappings
            builder.ToTable(nameof(HostGroupUserBillProfile));
        }
    }
}
