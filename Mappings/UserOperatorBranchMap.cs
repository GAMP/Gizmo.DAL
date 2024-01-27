using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// <see cref="UserOperatorBranch"/> mapping.
    /// </summary>
    public sealed class UserOperatorBranchMap : IEntityTypeConfiguration<UserOperatorBranch>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Configure(EntityTypeBuilder<UserOperatorBranch> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("OperatorBranchId")
                .HasColumnOrder(0);

            builder.Property(x => x.OperatorId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(x => x.BranchId)
              .IsRequired()
              .HasColumnOrder(2);

            builder.Property(x => x.IsDefault)
                .HasColumnOrder(3);

            builder.HasIndex(x => new { x.BranchId, x.OperatorId }).IsUnique();

            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.ModifiedBy)
                .WithMany()
                .HasForeignKey(x => x.ModifiedById);

            builder.HasOne(x => x.Operator)
                .WithMany(x => x.Branches)
                .HasForeignKey(x => x.OperatorId);

            builder.HasOne(x => x.Branch)
                .WithMany(x => x.Operators)
                .HasForeignKey(x => x.BranchId);

            builder.ToTable(nameof(UserOperatorBranch));
        }
    }
}
