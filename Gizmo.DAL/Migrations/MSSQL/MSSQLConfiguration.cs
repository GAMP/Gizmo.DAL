namespace Gizmo.DAL.Migrations.MSSQL
{
    using Gizmo.DAL.Contexts;

    using System.Data.Entity.Migrations;

    public sealed class MSSQLConfiguration : DbMigrationsConfiguration<DefaultDbContext>
    {
        public MSSQLConfiguration()
        {
            ContextType = typeof(DefaultDbContext);
            MigrationsNamespace = "Gizmo.DAL.Migrations.MSSQL";
            MigrationsDirectory = @"Migrations\MSSQL";
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new SqlServerCustomMigrationSqlGenerator());
            CommandTimeout = 360; //migration timeout, this usefull in cases where database creation or migration can take more time than default timeout
        }
    }
}
