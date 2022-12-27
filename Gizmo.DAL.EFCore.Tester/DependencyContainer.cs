using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gizmo.DAL.EFCore.Tester
{
    public static class DependencyContainer
    {
        public static IHost CreateHostDependencies()
        {
            const string connectionString = "";
            
            var host = Host.CreateDefaultBuilder();
            host.ConfigureLogging(logging => logging.ClearProviders());
            host.ConfigureServices(services =>
            {
                // For SQL Server Provider
                services.AddDbContext<DefaultDbContext>(options => options.UseSqlServer(connectionString,
                    x => x.MigrationsAssembly("Gizmo.DAL.EFCore.Migrations.MSSQL")));

                // For MySQL Provider 
                //services.AddDbContext<DefaultDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                //    x => x.MigrationsAssembly("Gizmo.DAL.EFCore.Migrations.MySQL")));

                // For Sqlite Provider
                //services.AddDbContext<DefaultDbContext>(options => options.UseSqlite(connectionString,
                //    x => x.MigrationsAssembly("Gizmo.DAL.EFCore.Migrations.Sqlit")));

                // For MySQL Provider
                //services.AddDbContext<DefaultDbContext>(options => options.UseNpgsql(connectionString,
                //    x => x.MigrationsAssembly("Gizmo.DAL.EFCore.Migrations.Npgsql")));
            });

            return host.Build();
        }
    }
}
