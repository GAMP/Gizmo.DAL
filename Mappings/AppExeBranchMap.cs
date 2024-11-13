using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// App exe branch entity map.
    /// </summary>
    public class AppExeBranchMap : IEntityTypeConfiguration<AppExeBranch>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppExeBranch> builder)
        {
            builder.ToTable(nameof(AppExeBranch));

            builder.HasKey(t => new { t.AppExeId, t.BranchId });

            builder.Property(e => e.AppExeId)
                .HasColumnOrder(0);
            builder.Property(e => e.BranchId)
                .HasColumnOrder(1);
            builder.Property(e => e.IsEnabled)
                .HasColumnOrder(2);

            builder.HasIndex(t => new { t.AppExeId, t.BranchId }).IsUnique().HasFilter(null);

            builder.HasOne(x => x.Branch)
                .WithMany(x => x.Executables)
                .HasForeignKey(x => x.BranchId);           
        }
    }
}
