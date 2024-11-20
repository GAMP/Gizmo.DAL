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

            builder.Property(branch => branch.HasWorkingSchedule)
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

            builder.Property(branch => branch.IsEnabled)
                .IsRequired()
                 .HasColumnOrder(19);

            builder.Property(branch => branch.IsDeleted)
                .IsRequired()
                .HasColumnOrder(20);

            builder.Property(branch => branch.Guid)
                .HasColumnOrder(21)
                .IsRequired();

            builder.HasIndex(branch => branch.Name)
                .IsUnique();

            builder.HasIndex(branch => branch.Guid)
                .IsUnique();

            builder.HasOne(branch => branch.CreatedBy)
                .WithMany(x => x.CreatedBranches)
                .HasForeignKey(x => x.CreatedById);

            builder.HasOne(branch => branch.ModifiedBy)
               .WithMany(userOperator => userOperator.ModifiedBranches)
               .HasForeignKey(branch => branch.ModifiedById);          
        }
    }
}
