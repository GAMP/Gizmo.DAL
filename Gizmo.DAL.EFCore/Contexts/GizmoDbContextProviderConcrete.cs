using System;

using GizmoDALV2;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using SharedLib;
using SharedLib.Configuration;

namespace Gizmo.DAL.Contexts
{
    /// <summary>
    /// Gizmo.DAL default db context ptovider.
    /// </summary>
    public sealed class GizmoDbContextProviderConcrete : IGizmoDbContextProviderConcrete, IGizmoDbContextProvider
    {
        private readonly ServiceDatabaseConfig _dbConfig;
        /// <summary>
        /// Gizmo.DAL default db context ptovider initializer.
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
            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();
            
            optionsBuilder.UseLazyLoadingProxies();
            
            switch (_dbConfig.DbType)
            {
                case DatabaseType.LOCALDB:
                case DatabaseType.MSSQLEXPRESS:
                case DatabaseType.MSSQL:
                    {
                        optionsBuilder.UseSqlServer(_dbConfig.DbConnectionString);
                        break;
                    }
                case DatabaseType.POSTGRE:
                    {
                        optionsBuilder.UseNpgsql(_dbConfig.DbConnectionString);
                        break;
                    }
                default:
                    throw new NotImplementedException(nameof(GetDbContext));
            };
            
            return new DefaultDbContext(optionsBuilder.Options);
        }

        /// <summary>
        /// Gets non-proxy database context.
        /// </summary>
        /// <returns>New context instance.</returns>
        public DefaultDbContext GetDbNonProxyContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();

            switch (_dbConfig.DbType)
            {
                case DatabaseType.LOCALDB:
                case DatabaseType.MSSQLEXPRESS:
                case DatabaseType.MSSQL:
                    {
                        optionsBuilder.UseSqlServer(_dbConfig.DbConnectionString);
                        break;
                    }
                case DatabaseType.POSTGRE:
                    {
                        optionsBuilder.UseNpgsql(_dbConfig.DbConnectionString);
                        break;
                    }
                default:
                    throw new NotImplementedException(nameof(GetDbContext));
            };
            
            return new DefaultDbContext(optionsBuilder.Options);
        }

        #endregion
    }
}
