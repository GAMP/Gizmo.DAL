using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System;
using GizmoDALV2;

namespace Gizmo.DAL.EFCore
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("UserId");

            builder.Property(t => t.FirstName)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(t => t.LastName)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.BirthDate);

            builder.Property(t => t.Address)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.City)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(t => t.Country)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(t => t.PostCode)
                .HasMaxLength(20);

            builder.Property(t => t.Phone)
                .HasMaxLength(20);

            builder.Property(t => t.MobilePhone)
                .HasMaxLength(20);

            builder.Property(x => x.Sex);

            builder.Property(x => x.SmartCardUID)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.Identification)
                .HasMaxLength(SQLStringSize.TINY);

            // Indexes
            builder.HasIndex(t => t.Guid).HasDatabaseName("UQ_Guid").IsUnique();
            builder.HasIndex(t => t.SmartCardUID).HasDatabaseName("UQ_SmartCardUID").IsUnique();
            builder.HasIndex(t => t.Identification).HasDatabaseName("UQ_Identification").IsUnique();

            // Relations
            builder.HasOne(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById);

            // Table & Column Mappings
            builder.ToTable("User");
        }
    }

    public class UserOperatorMap : IEntityTypeConfiguration<UserOperator>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserOperator> builder)
        {
            builder.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(t => t.Email)
                .HasMaxLength(254);

            // Indexes
            builder.HasIndex(t => t.Username).HasDatabaseName("UQ_Username").IsUnique().HasFilter(null);
            builder.HasIndex(t => t.Email).HasDatabaseName("UQ_Email").IsUnique();
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("UserOperator");

            // Seeds
            builder.HasData(new UserOperator
            {
                Id = 1,
                Username = "Admin",
                CreatedTime = new DateTime(2023, 01, 01),
                Guid = new Guid("691ea8b4-d794-4096-84ae-bbdb7bcc0b02"),
            });
        }
    }

    public class UserMemberMap : IEntityTypeConfiguration<UserMember>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserMember> builder)
        {
            builder.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(t => t.Email)
                .HasMaxLength(254);

            // Indexes
            builder.HasIndex(t => t.Username).HasDatabaseName("UQ_Username").IsUnique().HasFilter(null);

            builder.HasIndex(t => t.Email).HasDatabaseName("UQ_Email").IsUnique();
            
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable(nameof(UserMember));

            // Relationships
            builder.HasOne(t => t.UserGroup)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seeds
            builder.HasData(new UserMember() { Id = 2, Username = "User", UserGroupId = 1, Guid = new Guid("38753737-24f1-40d7-8ac4-ba61660d666a") });
        }
    }

    public class UserGuestMap : IEntityTypeConfiguration<UserGuest>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserGuest> builder)
        {
            // Table & Column Mappings
            builder.ToTable("UserGuest");

            builder.Property(x => x.IsJoined)
                .HasColumnOrder(1);

            builder.Property(x => x.IsReserved)
                .HasColumnOrder(2);

            builder.Property(x => x.ReservedHostId)
                .HasColumnOrder(3);

            builder.Property(x => x.ReservedSlot)
                .HasColumnOrder(4);

            // Indexes
            builder.HasIndex(x => new { x.ReservedHostId, x.ReservedSlot }).HasDatabaseName("UQ_UserGuestHostSlot").IsUnique();
            
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.ReservedHost)
                .WithMany(x => x.ReservedGuests)
                .HasForeignKey(x => x.ReservedHostId);
        }
    }
}
