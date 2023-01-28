using GizmoDALV2;
using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLib;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class UserGroupMap : IEntityTypeConfiguration<UserGroup>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.Name)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Description)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.AppGroupId)
                .IsRequired(false)
                .HasColumnOrder(3);

            builder.Property(x => x.SecurityProfileId)
                .IsRequired(false)
                .HasColumnOrder(4);

            builder.Property(x => x.BillProfileId)
                .HasColumnOrder(5);

            builder.Property(x => x.RequiredUserInfo)
                .HasColumnOrder(6);

            builder.Property(x => x.Overrides)
                .HasColumnOrder(7);

            builder.Property(x => x.Options)
                .HasColumnOrder(8);

            builder.Property(x => x.CreditLimitOptions)
                .HasColumnOrder(9);

            builder.Property(x => x.IsNegativeBalanceAllowed)
                .HasColumnOrder(10);

            builder.Property(x => x.CreditLimit)
                .HasColumnOrder(11);

            builder.Property(x => x.PointsAwardOptions)
                .HasColumnOrder(12);

            builder.Property(x => x.PointsMoneyRatio)
                .HasColumnOrder(13);

            builder.Property(x => x.PointsTimeRatio)
                .HasColumnOrder(14);

            builder.Property(x => x.Points)
                .HasColumnOrder(15);

            builder.Property(x => x.IsDefault)
                .HasColumnOrder(16);

            // Table & Column Mappings
            builder.ToTable(nameof(UserGroup));

            builder.Property(x => x.Id)
                .HasColumnName("UserGroupId");

            // Indexes
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();

            // Relationships
            builder.HasOne(x => x.AppGroup)
                .WithMany(x => x.UserGroups)
                .HasForeignKey(x => x.AppGroupId);

            builder.HasOne(x => x.SecurityProfile)
                .WithMany(x => x.UserGroups)
                .HasForeignKey(x => x.SecurityProfileId);

            builder.HasOne(x => x.BillProfile)
                .WithMany(x => x.UserGroups)
                .HasForeignKey(x => x.BillProfileId);

            // Seeds
            builder.HasData(new UserGroup() { Id = 1, Name = "Members", BillProfileId = 1, IsDefault = true });
            builder.HasData(new UserGroup() { Id = 2, Name = "Guests", Options = UserGroupOptionType.GuestUse, BillProfileId = 2 });
        }
    }
}
