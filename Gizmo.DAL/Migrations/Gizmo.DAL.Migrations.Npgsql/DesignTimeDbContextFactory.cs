using Gizmo.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Gizmo.DAL.Migrations.Npgsql
{
    /// <summary>
    /// Design-time factory for migration generation:
    /// <c>dotnet ef migrations add {Name} --project Gizmo.DAL.Migrations.Npgsql</c>.
    /// No database connection is made when adding migrations — the connection string
    /// is a placeholder giving the provider a valid shape.
    /// </summary>
    public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DefaultDbContext>
    {
        /// <inheritdoc/>
        public DefaultDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();

            optionsBuilder.UseNpgsql(
                "Host=localhost;Database=design_time_only",
                options => options.MigrationsAssembly("Gizmo.DAL.Migrations.Npgsql"));

            return new DefaultDbContext(optionsBuilder.Options);
        }
    }
}
