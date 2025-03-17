using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Host group entity map.
    /// </summary>
    public class HostGroupMap : IEntityTypeConfiguration<HostGroup>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<HostGroup> builder)
        {
            builder.HasKey(hostGroup => hostGroup.Id);

            builder.Property(hostGroup => hostGroup.Id)
                .HasColumnOrder(0);

            builder.Property(hostGroup => hostGroup.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(hostGroup => hostGroup.AppGroupId)
                .HasColumnOrder(2);

            builder.Property(hostGroup => hostGroup.SecurityProfileId)
                .HasColumnOrder(3);

            builder.Property(hostGroup => hostGroup.SkinName)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(hostGroup => hostGroup.Options)
                .HasColumnOrder(5);

            builder.Property(hostGroup => hostGroup.DefaultGuestGroupId)
                .HasColumnOrder(6)
                .IsRequired(false);

            builder.Property(hostGroup => hostGroup.BillProfileId)
                .HasColumnOrder(7)
                .IsRequired(false);

            builder.Property(hostGroup => hostGroup.ClientOptionsId)
                .HasColumnOrder(8)
                .IsRequired(false);

            builder.ToTable(nameof(HostGroup));

            builder.Property(hostGroup => hostGroup.Id)
                .HasColumnName("HostGroupId");

            builder.HasIndex(t => new { t.Name, t.BranchId })
                .IsUnique();

            builder.HasOne(hostGroup => hostGroup.AppGroup)
                .WithMany(appGroup => appGroup.HostGroups)
                .HasForeignKey(d => d.AppGroupId);

            builder.HasOne(hostGroup => hostGroup.SecurityProfile)
                .WithMany(securityProfile => securityProfile.HostGroups)
                .HasForeignKey(d => d.SecurityProfileId);

            builder.HasOne(hostGroup => hostGroup.BillProfile)
                .WithMany(billProfile => billProfile.HostGroups)
                .HasForeignKey(hostGroup => hostGroup.BillProfileId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(hostGroup => hostGroup.ClientOptions)
                .WithMany(clientOptions => clientOptions.HostGroups)
                .HasForeignKey(hostGroup => hostGroup.ClientOptionsId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
