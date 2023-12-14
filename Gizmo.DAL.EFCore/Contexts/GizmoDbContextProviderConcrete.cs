using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using SharedLib;
using SharedLib.Configuration;

namespace Gizmo.DAL.Contexts
{
    /// <summary>
    /// Gizmo.DAL default db context provider.
    /// </summary>
    public sealed class GizmoDbContextProviderConcrete : IGizmoDbContextProviderConcrete, IGizmoDbContextProvider
    {
        private readonly ServiceDatabaseConfig _dbConfig;
        /// <summary>
        /// Gizmo.DAL default db context provider initializer.
        /// </summary>
        /// <param name="options">DI options.</param>
        public GizmoDbContextProviderConcrete(IOptions<ServiceDatabaseConfig> options) => _dbConfig = options.Value;

        #region IGizmoDbContextProvider

        IGizmoDBContext IGizmoDbContextProvider.GetDbContext() => GetDbContext();
        IGizmoDBContext IGizmoDbContextProvider.GetDbNonProxyContext() => GetDbNonProxyContext();

        #endregion

        #region IGizmoDbContextProviderConcrete

        /// <summary>
        /// Gets database context.
        /// </summary>
        /// <returns>New context instance.</returns>
        public DefaultDbContext GetDbContext()
        {
            var optionsBuilder = CreateOptionsBuilder();
            
            return new DefaultDbContext(optionsBuilder.Options);
        }

        /// <summary>
        /// Gets non-proxy database context.
        /// </summary>
        /// <returns>New context instance.</returns>
        public DefaultDbContext GetDbNonProxyContext()
        {
            var optionsBuilder =  CreateOptionsBuilder();
            
            return new DefaultDbContext(optionsBuilder.Options);
        }

        #endregion

        private DbContextOptionsBuilder<DefaultDbContext> CreateOptionsBuilder() => _dbConfig.DbType switch
        {
            DatabaseType.LOCALDB or DatabaseType.MSSQLEXPRESS or DatabaseType.MSSQL => CreateMssqlOptionsBuilder(),
            DatabaseType.POSTGRE => CreateNpgsqlOptionsBuilder(),
            _ => throw new NotImplementedException(nameof(GetDbContext))
        };

        private DbContextOptionsBuilder<DefaultDbContext> CreateMssqlOptionsBuilder()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();
            
            optionsBuilder.UseSqlServer(_dbConfig.DbConnectionString, options =>
            {
                options.MigrationsAssembly("Gizmo.DAL.EFCore.Migrations.MSSQL");
            });
            
            return optionsBuilder;
        }

        private DbContextOptionsBuilder<DefaultDbContext> CreateNpgsqlOptionsBuilder()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();
            
            optionsBuilder.UseNpgsql(_dbConfig.DbConnectionString, options =>
            {
                options.MigrationsAssembly("Gizmo.DAL.EFCore.Migrations.Npgsql");
            });
            
            return optionsBuilder;
        }
    }
}
