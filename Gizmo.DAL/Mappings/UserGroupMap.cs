using Gizmo.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class UserGroupMap : EntityTypeConfiguration<UserGroup>
    {
        public UserGroupMap()
        {
            // Primary Key
            HasKey(x => x.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.Name)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            Property(x => x.Description)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            Property(x => x.AppGroupId)
                .IsOptional()
                .HasColumnOrder(3);

            Property(x => x.SecurityProfileId)
                .IsOptional()
                .HasColumnOrder(4);

            Property(x => x.BillProfileId)
                .HasColumnOrder(5);

            Property(x => x.RequiredUserInfo)
                .HasColumnOrder(6);

            Property(x => x.Overrides)
                .HasColumnOrder(7);

            Property(x => x.Options)
                .HasColumnOrder(8);

            Property(x => x.CreditLimitOptions)
                .HasColumnOrder(9);

            Property(x => x.IsNegativeBalanceAllowed)
                .HasColumnOrder(10);

            Property(x => x.CreditLimit)
                .HasColumnOrder(11);

            Property(x => x.PointsAwardOptions)
                .HasColumnOrder(12);

            Property(x => x.PointsMoneyRatio)
                .HasColumnOrder(13);

            Property(x => x.PointsTimeRatio)
                .HasColumnOrder(14);

            Property(x => x.Points)
                .HasColumnOrder(15);

            Property(x => x.IsDefault)
                .HasColumnOrder(16);

            // Table & Column Mappings
            ToTable(nameof(UserGroup));

            Property(x => x.Id)
                .HasColumnName("UserGroupId");

            // Relationships
            HasOptional(x => x.AppGroup)
                .WithMany(x => x.UserGroups)
                .HasForeignKey(x => x.AppGroupId);

            HasOptional(x => x.SecurityProfile)
                .WithMany(x => x.UserGroups)
                .HasForeignKey(x => x.SecurityProfileId);

            HasOptional(x => x.BillProfile)
                .WithMany(x => x.UserGroups)
                .HasForeignKey(x => x.BillProfileId);
        }
    }
}
