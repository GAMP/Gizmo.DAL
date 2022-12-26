using GizmoDALV2.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasColumnName("UserId");

            Property(t => t.FirstName)
                .HasMaxLength(SQLStringSize.TINY45);

            Property(t => t.LastName)
                .HasMaxLength(SQLStringSize.TINY45);

            Property(x => x.BirthDate);

            Property(t => t.Address)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.City)
                .HasMaxLength(SQLStringSize.TINY45);

            Property(t => t.Country)
                .HasMaxLength(SQLStringSize.TINY45);

            Property(t => t.PostCode)
                .HasMaxLength(20);

            Property(t => t.Phone)
                .HasMaxLength(20);

            Property(t => t.MobilePhone)
                .HasMaxLength(20);

            Property(x => x.Sex);

            Property(x => x.SmartCardUID)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_SmartCardUID") { IsUnique = true }
                }));

            Property(x => x.Identification)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Identification") { IsUnique = true }
                }));

            Property(t => t.Guid)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true }
                }));

            // Table & Column Mappings
            ToTable("User");
        }
    }

    public class UserOperatorMap : EntityTypeConfiguration<UserOperator>
    {
        public UserOperatorMap()
        {
            Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Username") { IsUnique = true }
                }));

            Property(t => t.Email)
                .HasMaxLength(254)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Email") { IsUnique = true}
                }));

            // Table & Column Mappings
            ToTable("UserOperator");
        }
    }

    public class UserMemberMap : EntityTypeConfiguration<UserMember>
    {
        public UserMemberMap()
        {
            Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Username") { IsUnique = true }
                }));

            Property(t => t.Email)
                .HasMaxLength(254)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Email") { IsUnique = true}
                }));


            // Table & Column Mappings
            ToTable(nameof(UserMember));

            // Relationships
            HasRequired(t => t.UserGroup)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.UserGroupId)
                .WillCascadeOnDelete(false);
        }
    }

    public class UserGuestMap : EntityTypeConfiguration<UserGuest>
    {
        public UserGuestMap()
        {
            // Table & Column Mappings
            ToTable("UserGuest");

            Property(x => x.IsJoined)
                .HasColumnOrder(1);

            Property(x => x.IsReserved)
                .HasColumnOrder(2);

            Property(x => x.ReservedHostId)
                .HasColumnOrder(3)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_UserGuestHostSlot") { IsUnique = true, Order = 0 } }));

            Property(x => x.ReservedSlot)
                .HasColumnOrder(4)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_UserGuestHostSlot") { IsUnique = true, Order = 1 } }));

            HasOptional(x => x.ReservedHost)
                .WithMany(x => x.ReservedGuests)
                .HasForeignKey(x => x.ReservedHostId);
        }
    }
}
