using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Recipient channeled map.
    /// </summary>
    public sealed class RecipientChanneledMap : IEntityTypeConfiguration<RecipientChanneled>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<RecipientChanneled> builder)
        {
            builder.ToTable(nameof(RecipientChanneled))
                .HasBaseType<Recipient>();

            builder.HasMany(recipientChanneled => recipientChanneled.Channels)
                .WithOne(recipientChannel => recipientChannel.RecipientChanneled)
                .HasForeignKey(recipientChannel => recipientChannel.RecipientChanneledId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
