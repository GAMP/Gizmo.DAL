using System;

namespace Gizmo.DAL.Scripts
{
    internal static class NpgSqlScripts
    {
        internal static string GetScript(string scriptName) => scriptName switch
        {
            _ => throw new NotSupportedException($"Script name {scriptName} is not supported for this database provider."),
        };
    }
}
