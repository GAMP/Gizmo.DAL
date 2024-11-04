using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class NewsBranchMap : IEntityTypeConfiguration<NewsBranch>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<NewsBranch> builder)
        {
            builder.HasKey(t => new { t.NewsId, t.BranchId });

            builder.Property(e => e.NewsId)
                .HasColumnOrder(0);
            builder.Property(e => e.BranchId)
                .HasColumnOrder(1);
            builder.Property(e => e.IsEnabled)
                .HasColumnOrder(3);

            builder.HasIndex(t => new { t.NewsId, t.BranchId }).IsUnique().HasFilter(null);

            builder.HasOne(x => x.Branch)
                .WithMany(x => x.News)
                .HasForeignKey(x => x.BranchId);

            builder.ToTable(nameof(NewsBranch));
        }
    }
}
