using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        }
    }
}
