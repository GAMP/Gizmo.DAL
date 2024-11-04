using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class AppExeBranchMap : IEntityTypeConfiguration<AppExeBranch>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppExeBranch> builder)
        {
            builder.HasKey(t => new { t.AppExeId, t.BranchId });

            builder.Property(e => e.AppExeId)
                .HasColumnOrder(0);
            builder.Property(e => e.BranchId)
                .HasColumnOrder(1);
            builder.Property(e => e.IsEnabled)
                .HasColumnOrder(3);

            builder.HasIndex(t => new { t.AppExeId, t.BranchId }).IsUnique().HasFilter(null);

            builder.HasOne(x => x.Branch)
                .WithMany(x => x.Executables)
                .HasForeignKey(x => x.BranchId);

            builder.ToTable(nameof(AppExeBranch));
        }
    }
}
