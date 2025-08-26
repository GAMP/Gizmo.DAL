namespace Gizmo.DAL.Tests.Containers;

public sealed class Configuration
{
    public string Image { get; init; } = string.Empty;
    public string User { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public Backup Backup { get; init; } = new Backup();
}

public sealed class Backup
{
    public string Src { get; init; } = string.Empty;
    public string Dst { get; init; } = string.Empty;
}
