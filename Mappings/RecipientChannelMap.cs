using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Recipient channel map.
    /// </summary>
    public sealed class RecipientChannelMap : IEntityTypeConfiguration<RecipientChannel>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<RecipientChannel> builder)
        {
            builder.ToTable(nameof(RecipientChannel));
            builder.HasKey(recipientChannel => recipientChannel.Id);

            builder.Property(recipientChannel => recipientChannel.Id)
                .HasColumnName("RecipientChannelId")
                .HasColumnOrder(0);

            builder.Property(recipientChannel => recipientChannel.RecipientChanneledId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(recipientChannel => recipientChannel.ChannelType)
                .IsRequired()
                .HasColumnOrder(2);

        }
    }
}
