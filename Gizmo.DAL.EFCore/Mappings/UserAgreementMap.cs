using Gizmo.DAL.Entities;
using GizmoDALV2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class UserAgreementMap : IEntityTypeConfiguration<UserAgreement>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserAgreement> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("UserAgreementId");

            builder.Property(x => x.Name)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired();

            builder.Property(x => x.Agreement)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(x => x.Options)
                .HasColumnOrder(3);

            builder.Property(x => x.DisplayOptions)
                .HasColumnOrder(4);

            builder.Property(x => x.DisplayOrder)
                .HasColumnOrder(5);

            builder.Property(x => x.IsEnabled)
                .HasColumnOrder(6);

            // Table & Column Mappings
            builder.ToTable(nameof(UserAgreement));
        }
    }
}
