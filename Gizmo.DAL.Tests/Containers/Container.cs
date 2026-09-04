using System;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Configuration;
using Testcontainers.MsSql;
using Testcontainers.PostgreSql;

namespace Gizmo.DAL.Tests.Containers;

public static class Container
{
    public static async Task<CreationResult<T>> Start<T>() where T : DockerContainer, IDatabaseContainer => typeof(T) switch
    {
        Type t when t == typeof(PostgreSqlContainer) => await CreatePgSql() as CreationResult<T>,
        Type t when t == typeof(MsSqlContainer) => await CreateMsSql() as CreationResult<T>,
        _ => throw new NotSupportedException($"Database type {typeof(T)} is not supported.")
    }
    ?? throw new InvalidOperationException($"Failed to create container of type {typeof(T)}.");

    public static async Task Stop(DockerContainer? container)
    {
        if (container is not null)
        {
            await container.StopAsync();
            await container.DisposeAsync();
        }
    }

    private static Configuration CreateConfiguration(string section) => new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build()
        .GetSection(section)
        .Get<Configuration>();

    private static async Task<T> Start<B, T>(B containerBuilder)
         where B : IContainerBuilder<B, T>
         where T : DockerContainer, IDatabaseContainer
    {
        // Disposable current-schema harness: containers never bind-mount a host backup source.
        // Per-test databases and the optional ticker schema are created inside the container from
        // the current EF model, so the harness reads or writes no host backup directory or data.
        var container = containerBuilder.Build();
        await container.StartAsync();
        return container;
    }

    private static async Task<CreationResult<PostgreSqlContainer>> CreatePgSql()
    {
        var configuration = CreateConfiguration("PostgreSql");

        var containerBuilder = new PostgreSqlBuilder()
           .WithUsername(configuration.User)
           .WithImage(configuration.Image)
           .WithPassword(configuration.Password);

        var container = await Start<PostgreSqlBuilder, PostgreSqlContainer>(containerBuilder);

        Environment.SetEnvironmentVariable("POSTGRES_DOCKER", container.Name[1..]); // reduce / from start

        return new CreationResult<PostgreSqlContainer>(container);
    }

    private static async Task<CreationResult<MsSqlContainer>> CreateMsSql()
    {
        var configuration = CreateConfiguration("MsSql");

        var containerBuilder = new MsSqlBuilder()
            .WithEnvironment("MSSQL_PID", "Developer")
            .WithEnvironment("MSSQL_AGENT_ENABLED", "true")
            .WithImage(configuration.Image)
            .WithPassword(configuration.Password);

        var container = await Start<MsSqlBuilder, MsSqlContainer>(containerBuilder);
        return new CreationResult<MsSqlContainer>(container);
    }
}
