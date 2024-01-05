using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class AttributeMap : IEntityTypeConfiguration<Entities.Attribute>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Entities.Attribute> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("AttributeId"); ;

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnOrder(1);

            builder.Property(x => x.FriendlyName)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);


            // Indexes
            builder.HasIndex(t => t.Name).IsUnique();

            // Table & Column Mappings
            builder.ToTable(nameof(Entities.Attribute));
        }
    }
}
