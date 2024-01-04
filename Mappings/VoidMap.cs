using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class VoidMap : IEntityTypeConfiguration<Entities.Void>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Entities.Void> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnName("VoidId")
                .HasColumnOrder(0);

            // Table & Column Mappings
            builder.ToTable(nameof(Entities.Void));
        }
    }   
}
