using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class UsageBaseMap : IEntityTypeConfiguration<Usage>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Usage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("UsageId");

            builder.Property(x => x.UsageSessionId)
                .HasColumnOrder(1);

            builder.Property(x => x.UserId)
                .HasColumnOrder(2);

            builder.Property(x => x.Seconds)
                .HasColumnOrder(3);

            builder.ToTable(nameof(Usage));

            builder.HasOne(x => x.UsageSession)
                .WithMany(x => x.Usage)
                .HasForeignKey(x => x.UsageSessionId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Usage)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
