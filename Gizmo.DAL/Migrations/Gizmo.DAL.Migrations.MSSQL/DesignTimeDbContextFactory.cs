using Gizmo.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <summary>
    /// Design-time factory for migration generation:
    /// <c>dotnet ef migrations add {Name} --project Gizmo.DAL.Migrations.MSSQL</c>.
    /// No database connection is made when adding migrations — the connection string
    /// is a placeholder giving the provider a valid shape.
    /// </summary>
    public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DefaultDbContext>
    {
        /// <inheritdoc/>
        public DefaultDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=design_time_only;Integrated Security=True;TrustServerCertificate=True",
                options => options.MigrationsAssembly("Gizmo.DAL.Migrations.MSSQL"));

            return new DefaultDbContext(optionsBuilder.Options);
        }
    }
}
