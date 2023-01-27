using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class DiscountUserMap : IEntityTypeConfiguration<DiscountUser>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<DiscountUser> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnOrder(0)
                .HasColumnName("DiscountUserId");  

            // Table & Column Mappings
            builder.ToTable("DiscountUser");

            // Relationships

            builder.HasOne(x => x.UserGroup)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Discount)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
