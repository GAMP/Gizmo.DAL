using GizmoDALV2;
using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLib;

namespace Gizmo.DAL.Mappings
{
    public class PaymentMethodMap : IEntityTypeConfiguration<PaymentMethod>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasColumnName("PaymentMethodId")
                .HasColumnOrder(0);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnOrder(1);

            builder.Property(x => x.Description)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(2);

            builder.Property(x => x.Surcharge)
                .HasColumnOrder(3);

            builder.Property(x => x.DisplayOrder)
                .HasColumnOrder(4);

            builder.Property(x => x.IsEnabled)
                .HasColumnOrder(5);

            builder.Property(x => x.Options)
                .HasColumnOrder(6);

            builder.Property(x => x.IsClient)
                .HasColumnOrder(7);

            builder.Property(x => x.IsManager)
                .HasColumnOrder(8);

            builder.Property(x => x.IsPortal)
                .HasColumnOrder(9);

            builder.Property(x => x.IsDeleted)
                .HasColumnOrder(10);

            builder.Property(x => x.PaymentProvider)
                .IsRequired(false)
                .HasColumnOrder(11);

            // Indexes
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();
            
            // Table & Column Mappings
            builder.ToTable("PaymentMethod");
        }
    }
}
