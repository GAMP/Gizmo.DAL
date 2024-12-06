using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Recipient map.
    /// </summary>
    public sealed class RecipientMap : IEntityTypeConfiguration<Recipient>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<Recipient> builder)
        {
            builder.ToTable(nameof(Recipient))
                .UseTptMappingStrategy();

            builder.HasKey(recipient => recipient.Id);
            builder.Property(recipient => recipient.Id)
                .HasColumnName("RecipientId")
                .HasColumnOrder(0);
        }
    }
}
