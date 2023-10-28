using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Gizmo.DAL.Mappings
{
    public class HostEndpointMap : IEntityTypeConfiguration<HostEndpoint>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<HostEndpoint> builder)
        {
            // Table & Column Mappings
            builder.ToTable("HostEndpoint");

            // Indexes
            builder.HasIndex(x => x.Id);

            // Seeds
            builder.HasData(new HostEndpoint() { Id = 1, Name = "XBOX-ONE-1", Number = 1, MaximumUsers = 4, HostGroupId = 2, Guid = new Guid("cd41aa25-ac1f-4da9-8c8e-075032803871") });
            builder.HasData(new HostEndpoint() { Id = 2, Name = "XBOX-ONE-2", Number = 2, MaximumUsers = 4, HostGroupId = 2, Guid = new Guid("cd41aa25-ac1f-4da9-8c8e-075032803872") });
            builder.HasData(new HostEndpoint() { Id = 3, Name = "PS4-1", Number = 3, MaximumUsers = 4, HostGroupId = 2 , Guid = new Guid("cd41aa25-ac1f-4da9-8c8e-075032803873") });
            builder.HasData(new HostEndpoint() { Id = 4, Name = "WII-1", Number = 4, MaximumUsers = 4, HostGroupId = 2 , Guid = new Guid("cd41aa25-ac1f-4da9-8c8e-075032803874") });
        }
    }
}
