using GizmoDALV2.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class UserOperatorUserGroupAcessMap : EntityTypeConfiguration<UserOperatorUserGroupAccess>
    {
        public UserOperatorUserGroupAcessMap()
        {
            ToTable(nameof(UserOperatorUserGroupAccess));

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnOrder(0)
                .HasColumnName("UserOperatorUserGroupAcessId")
                .IsRequired();

            Property(e => e.OperatorId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_UserOperator_UserGroup") { IsUnique = true, Order = 0 }
                }))
                .IsRequired();

            Property(e => e.UserGroupId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_UserOperator_UserGroup") { IsUnique = true, Order = 1 }
                }))
                .IsRequired();

            Property(e => e.DisallowLogin)
                .HasColumnOrder(3)
                .IsRequired();

            Property(e => e.DisallowMove)
                .HasColumnOrder(4)
                .IsRequired();

            //HasRequired(e => e.Operator)
            //    .WithMany(e => e.UserGroupAccess)
            //    .HasForeignKey(e => e.OperatorId);

            HasRequired(e => e.UserGroup)
                .WithMany()
                .WillCascadeOnDelete(false);

            HasRequired(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .WillCascadeOnDelete(false);

            HasRequired(e => e.ModifiedBy)
                .WithMany()
                .HasForeignKey(e => e.ModifiedById)
                .WillCascadeOnDelete(false);
        }
    }
}
