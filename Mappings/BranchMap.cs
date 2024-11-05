using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// <see cref="Branch"/> mapping.
    /// </summary>
    public sealed class BranchMap : IEntityTypeConfiguration<Branch>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasIndex(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("BranchId");

            builder.Property(x => x.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.City)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Address)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.PostalCode)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Region)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Latitude)
                .HasColumnOrder(6)
                .HasPrecision(9, 6);

            builder.Property(x => x.Longitude)
                .HasColumnOrder(7)
                .HasPrecision(9, 6);

            builder.Property(x => x.Phone)
                .HasColumnOrder(8)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Email)
                .HasColumnOrder(9)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.WebSite)
                .HasColumnOrder(10)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.Info)
                .HasColumnOrder(11)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.TimeZone)
                .HasColumnOrder(12)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.IsEnabled)
                 .HasColumnOrder(13);

            builder.Property(x => x.IsDeleted)
                .HasColumnOrder(14);

            builder.Property(x => x.Guid)
                .HasColumnOrder(15)
                .IsRequired();

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.HasIndex(x => x.Guid)
                .IsUnique();

            builder.HasOne(x => x.CreatedBy)
                .WithMany(x => x.CreatedBranches)
                .HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.ModifiedBy)
               .WithMany(x => x.ModifiedBranches)
               .HasForeignKey(x => x.ModifiedById);

            builder.ToTable(nameof(Branch));
        }
    }
}
