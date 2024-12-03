using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// <see cref="Branch"/> entity mapping.
    /// </summary>
    public sealed class BranchMap : IEntityTypeConfiguration<Branch>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable(nameof(Branch));

            builder.HasIndex(branch => branch.Id);

            builder.Property(branch => branch.Id)
                .HasColumnOrder(0)
                .HasColumnName("BranchId");

            builder.Property(branch => branch.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(branch => branch.Country)
                .HasColumnOrder(2)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(branch => branch.City)
                .HasColumnOrder(3)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(branch => branch.Address)
                .HasColumnOrder(4)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(branch => branch.PostalCode)
                .HasColumnOrder(5)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(branch => branch.Region)
                .HasColumnOrder(6)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(branch => branch.Latitude)
                .HasColumnOrder(7)
                .IsRequired()
                .HasPrecision(9, 6);

            builder.Property(branch => branch.Longitude)
                .HasColumnOrder(8)
                .IsRequired()
                .HasPrecision(9, 6);

            builder.Property(branch => branch.Phone)
                .HasColumnOrder(9)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(branch => branch.Email)
                .HasColumnOrder(10)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(branch => branch.WebSite)
                .HasColumnOrder(11)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(branch => branch.Info)
                .HasColumnOrder(12)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(branch => branch.TimeZone)
                .HasColumnOrder(13)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(branch => branch.HasBusinessSchedule)
                .IsRequired()
                .HasColumnOrder(14);

            builder.Property(branch => branch.BusinessDayStart)
              .IsRequired(false)
              .HasColumnOrder(15);

            builder.Property(branch => branch.BusinessDayEnd)
                .IsRequired(false)
                .HasColumnOrder(16);

            builder.Property(branch => branch.BusinessStartWeekDay)
                .IsRequired(false)
                .HasColumnOrder(17);

            builder.Property(branch => branch.BusinessEndWeekDay)
                .IsRequired(false)
                .HasColumnOrder(18);

            builder.Property(branch => branch.IsFiscalizationEnabled)
                .IsRequired(false)
                .HasColumnOrder(19);

            builder.Property(branch => branch.BusinessVATId)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnOrder(20);

            builder.Property(branch => branch.TaxSystem)
                .IsRequired(false)
                .HasColumnOrder(21);

            builder.Property(branch => branch.GoodsTaxSystem)
                .IsRequired(false)
                .HasColumnOrder(22);

            builder.Property(branch => branch.ServicesTaxSystem)
                .IsRequired(false)
                .HasColumnOrder(23);

            builder.Property(branch => branch.TreatDepositsAsService)
                .IsRequired(false)
                .HasColumnOrder(24);

            builder.Property(branch => branch.DepositServiceDescription)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(25);

            builder.Property(branch => branch.TimeBasedServiceVATRate)
                .IsRequired(false)
                .HasColumnOrder(26);

            builder.Property(branch => branch.DepositVATRate)
                .IsRequired(false)
                .HasColumnOrder(27);

            builder.Property(branch => branch.DepositAdvancePaymentType)
                .IsRequired(false)
                .HasColumnOrder(28);

            builder.Property(branch => branch.CompanionId)
                .IsRequired(false)
                .HasColumnOrder(29);

            builder.Property(branch => branch.Guid)
                .HasColumnOrder(30)
                .IsRequired();

            builder.Property(branch => branch.IsDisabled)
                .IsRequired()
                .HasColumnOrder(31);

            builder.Property(branch => branch.DisableTime)
                .IsRequired(false)
                .HasColumnOrder(32);

            builder.Property(branch => branch.IsDeleted)
                .IsRequired()
                .HasColumnOrder(33);

            builder.HasIndex(branch => branch.Name)
                .IsUnique();

            builder.HasIndex(branch => branch.Guid)
                .IsUnique().HasFilter(null);

            builder.HasOne(branch => branch.CreatedBy)
                .WithMany(x => x.CreatedBranches)
                .HasForeignKey(x => x.CreatedById);

            builder.HasOne(branch => branch.ModifiedBy)
               .WithMany(userOperator => userOperator.ModifiedBranches)
               .HasForeignKey(branch => branch.ModifiedById);
        }
    }
}
