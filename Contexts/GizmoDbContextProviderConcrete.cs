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

        /// <summary>
        /// Gizmo.DAL default db context provider initializer.
        /// </summary>
        /// <param name="dbConfig">
        /// Database configuration.
        /// </param>
        public GizmoDbContextProviderConcrete(ServiceDatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig ?? throw new ArgumentNullException(nameof(dbConfig));
        }

        #region IGizmoDbContextProvider

        IGizmoDBContext IGizmoDbContextProvider.GetDbContext() => GetDbContext();
        IGizmoDBContext IGizmoDbContextProvider.GetDbNonProxyContext() => GetDbNonProxyContext();

        #endregion

        #region IGizmoDbContextProviderConcrete

        /// <inheritdoc/>
        public DefaultDbContext GetDbContext()
        {
            var optionsBuilder = CreateOptionsBuilder(_dbConfig);
            return new DefaultDbContext(optionsBuilder.Options);
        }

        /// <inheritdoc/>
        public DefaultDbContext GetDbNonProxyContext()
        {
            var optionsBuilder =  CreateOptionsBuilder(_dbConfig);
            return new DefaultDbContext(optionsBuilder.Options);
        }

        /// <inheritdoc/>
        public DefaultDbContext GetDbContext(ServiceDatabaseConfig dbConfig)
        {
            var optionsBuilder = CreateOptionsBuilder(dbConfig);
            return new DefaultDbContext(optionsBuilder.Options);
        }

        /// <inheritdoc/>
        public DefaultDbContext GetDbNonProxyContext(ServiceDatabaseConfig dbConfig)
        {
            var optionsBuilder = CreateOptionsBuilder(dbConfig);
            return new DefaultDbContext(optionsBuilder.Options);
        }

        #endregion
        
        private static DbContextOptionsBuilder<DefaultDbContext> CreateOptionsBuilder(ServiceDatabaseConfig dbConfig) => dbConfig.DbType switch
        {
            DatabaseType.LOCALDB or DatabaseType.MSSQLEXPRESS or DatabaseType.MSSQL => 
                CreateMssqlOptionsBuilder(dbConfig.DbConnectionString, dbConfig.CommandTimeout),
            DatabaseType.POSTGRE => 
                CreateNpgsqlOptionsBuilder(dbConfig.DbConnectionString, dbConfig.CommandTimeout),
            _ => throw new NotImplementedException(nameof(GetDbContext))
        };
        private static DbContextOptionsBuilder<DefaultDbContext> CreateMssqlOptionsBuilder(string connectionString, int? commandTimeout = 180)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));
            
            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();
            
            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                options.CommandTimeout(commandTimeout);
                options.MigrationsAssembly("Gizmo.DAL.Migrations.MSSQL");
            });
            
            return optionsBuilder;
        }
        private static DbContextOptionsBuilder<DefaultDbContext> CreateNpgsqlOptionsBuilder(string connectionString, int? commandTimeout = 180)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();
            
            optionsBuilder.UseNpgsql(connectionString, options =>
            {
                options.CommandTimeout(commandTimeout);
                options.MigrationsAssembly("Gizmo.DAL.Migrations.Npgsql");
            });
            
            return optionsBuilder;
        }
    }
}
