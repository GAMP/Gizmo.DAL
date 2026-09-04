using DotNet.Testcontainers.Containers;

namespace Gizmo.DAL.Tests.Containers;

public sealed record CreationResult<T>(T Container) where T :DockerContainer, IDatabaseContainer;
