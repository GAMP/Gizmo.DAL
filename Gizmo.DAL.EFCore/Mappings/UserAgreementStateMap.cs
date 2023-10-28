using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class UserAgreementStateMap : IEntityTypeConfiguration<UserAgreementState>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserAgreementState> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            builder.Property(x => x.Id)
                    .HasColumnOrder(0)
                    .HasColumnName("UserAgreementStateId");

            builder.Property(x => x.UserAgreementId)
                .HasColumnOrder(1);
                
            builder.Property(x => x.AcceptState)
                .HasColumnOrder(2);

            // Indexes
            builder.HasIndex(x => new { x.UserAgreementId, x.UserId }).HasDatabaseName("UQ_UserAgreementState").IsUnique();

            builder.HasOne(x => x.UserAgreement)
                .WithMany(x => x.UserAgreementStates)
                .HasForeignKey(x => x.UserAgreementId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserAgreementStates)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById);

            builder.ToTable(nameof(Entities.UserAgreementState));
        }
    }
}
