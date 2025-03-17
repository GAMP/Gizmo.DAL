using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// User api key mapping.
    /// </summary>
    public sealed class UserApiKeyMap : IEntityTypeConfiguration<UserApiKey>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<UserApiKey> builder)
        {
            builder.ToTable(nameof(UserApiKey));

            builder.Property(userApiKey => userApiKey.Id)
                .HasColumnOrder(0);

            builder.Property(userApiKey => userApiKey.ApiKey)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(UserApiKey => UserApiKey.Type)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(UserApiKey => UserApiKey.ExpireTime)
                .HasColumnOrder(3)
                .IsRequired(false);

            builder.HasIndex(userApiKey => userApiKey.ApiKey)
                .IsUnique()
                .HasFilter(null);
        }
    }
}
