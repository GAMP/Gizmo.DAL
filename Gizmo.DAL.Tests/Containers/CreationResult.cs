using DotNet.Testcontainers.Containers;

namespace Gizmo.DAL.Tests.Containers;

public sealed record CreationResult<T>(T Container, Configuration Config) where T :DockerContainer, IDatabaseContainer;
