using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class UserOperatorUserGroupAcessMap : IEntityTypeConfiguration<UserOperatorUserGroupAccess>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserOperatorUserGroupAccess> builder)
        {
            builder.ToTable(nameof(UserOperatorUserGroupAccess));

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnOrder(0)
                .HasColumnName("UserOperatorUserGroupAcessId")
                .IsRequired();

            builder.Property(e => e.OperatorId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(e => e.UserGroupId)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(e => e.DisallowLogin)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(e => e.DisallowMove)
                .HasColumnOrder(4)
                .IsRequired();

            // Indexes
            builder.HasIndex(x => new { x.OperatorId, x.UserGroupId }).HasDatabaseName("UQ_UserOperator_UserGroup").IsUnique();

            //builder.HasOne(e => e.Operator)
            //    .WithMany(e => e.UserGroupAccess)
            //    .HasForeignKey(e => e.OperatorId);

            builder.HasOne(e => e.UserGroup)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ModifiedBy)
                .WithMany()
                .HasForeignKey(e => e.ModifiedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
