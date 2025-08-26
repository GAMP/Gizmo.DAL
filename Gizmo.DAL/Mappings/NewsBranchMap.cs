using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// News branch entity map.
    /// </summary>
    public class NewsBranchMap : IEntityTypeConfiguration<NewsBranch>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<NewsBranch> builder)
        {
            builder.ToTable(nameof(NewsBranch));

            builder.HasKey(t => new { t.NewsId, t.BranchId });

            builder.Property(e => e.NewsId)
                .HasColumnOrder(0);

            builder.Property(e => e.BranchId)
                .HasColumnOrder(1);

            builder.Property(e => e.IsEnabled)
                .HasColumnOrder(2);

            builder.HasIndex(t => new { t.NewsId, t.BranchId })
                .IsUnique();

            builder.HasOne(x => x.Branch)
                .WithMany(x => x.News)
                .HasForeignKey(x => x.BranchId);
        }
    }
}
