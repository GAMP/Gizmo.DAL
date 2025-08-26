using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class AppEnterpriseMap : IEntityTypeConfiguration<AppEnterprise>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppEnterprise> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(t => t.Id)
              .HasColumnName("AppEnterpriseId");

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(t => t.Guid)
                .HasColumnOrder(2);

            // Indexes
            builder.HasIndex(t => t.Guid).IsUnique();

            builder.HasIndex(t => t.Name).IsUnique();

            // Table & Column Mappings
            builder.ToTable("AppEnterprise");
        }
    }
}
