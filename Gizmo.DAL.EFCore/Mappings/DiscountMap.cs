using GizmoDALV2;
using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class DiscountBaseMap : IEntityTypeConfiguration<DiscountBase>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<DiscountBase> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnOrder(0)
                .HasColumnName("DiscountId");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Value)
                .HasColumnOrder(2);

            builder.Property(x => x.DiscountAmountType)
                .HasColumnOrder(3);            

            // Indexes
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();

            // Table & Column Mappings
            builder.ToTable("DiscountBase");

            // Relationships
        }
    }
}
