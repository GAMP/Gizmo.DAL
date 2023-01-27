using GizmoDALV2;
using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class UserAttributeMap : IEntityTypeConfiguration<UserAttribute>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserAttribute> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("UserAttributeId"); ;

            builder.Property(x => x.UserId)
                .HasColumnOrder(1);

            builder.Property(x => x.AttributeId)
                .HasColumnOrder(2);

            builder.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(3);

            // Indexes
            builder.HasIndex(x => new { x.UserId, x.AttributeId }).HasDatabaseName("UQ_UserAttribute").IsUnique();

            // Relations
            builder.HasOne(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById);

            // Table & Column Mappings
            builder.ToTable(nameof(UserAttribute));

            //Relations
            builder.HasOne(x => x.User)
                .WithMany(x => x.Attributes);
        }
    }
}
