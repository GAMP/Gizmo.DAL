# Function to check if 'dotnet ef' is available
function Test-DotnetEf {
    try {
        dotnet ef
        return $true
    } catch {
        return $false
    }
}

# Install EF tools if 'dotnet ef' is not available
if (-not (Test-DotnetEf)) {
    dotnet tool install --global dotnet-ef
}

$dbContextProject = .\..\..\..\EFCoreMigration\EFCoreTest\EFCoreTest.csproj
$migrationsEF6Path = .\Gizmo.DAL.EF6.Migrations.MSSQL
$migrationsEFCorePath = .\Gizmo.DAL.EFCore.Migrations.MSSQL
$initMigrationName = "20231113182556_Initial"

$currentMigration = dotnet ef migrations list `
    --project $migrationsEFCorePath `
    --startup-project $dbContextProject `
    --output json | ConvertFrom-Json | Select-Object -Last 1

# Revert migrations
dotnet ef database update $initMigration
    
while ($currentMigration -ne $initMigration) {
    dotnet ef migrations remove --project .\YourDbContextProject.csproj --startup-project .\YourStartupProject.csproj
    $currentMigration = dotnet ef migrations list --project .\YourDbContextProject.csproj --startup-project .\YourStartupProject.csproj --output json | ConvertFrom-Json | Select-Object -Last 1
}

Write-Host "All migrations have been reverted to $initMigration"