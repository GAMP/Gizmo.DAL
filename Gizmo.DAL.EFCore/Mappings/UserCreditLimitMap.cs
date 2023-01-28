using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class UserCreditLimitMap : IEntityTypeConfiguration<UserCreditLimit>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserCreditLimit> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .ValueGeneratedNever();

            builder.Property(x => x.CreditLimit);

            // Indexes
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("UserCreditLimit");

            builder.Property(t => t.Id).
                HasColumnName("UserId");

            // Relationships
            builder.HasOne(t => t.User)
                .WithOne(t => t.UserCreditLimit)
                .HasForeignKey<UserCreditLimit>(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
