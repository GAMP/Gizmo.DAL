using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Age restriction map.
    /// </summary>
    public sealed class AgeRestrictionMap : IEntityTypeConfiguration<AgeRestriction>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<AgeRestriction> builder)
        {
            builder.ToTable(nameof(AgeRestriction))
                .UseTptMappingStrategy();

            builder.HasKey(ageRestriction => ageRestriction.Id);
            
            builder.Property(AgeRestriction => AgeRestriction.Id)
                .HasColumnName("AgeRestrictionId")
                .HasColumnOrder(0);

            builder.Property(ageLoginRestriction => ageLoginRestriction.AgeFrom)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(ageLoginRestriction => ageLoginRestriction.AgeTo)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(ageLoginRestriction => ageLoginRestriction.DayMinuteFrom)
                .IsRequired(false)
                .HasColumnOrder(3);

            builder.Property(ageLoginRestriction => ageLoginRestriction.DayMinuteTo)
                .IsRequired(false)
                .HasColumnOrder(4);
        }
    }
}
