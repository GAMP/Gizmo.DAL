function Write-Info($message){
    Write-Host "`n$message`n" -ForegroundColor Yellow
}
function Write-Error($message){
    Write-Host "`n$message`n" -ForegroundColor Red

}
$toolsName = "Dotnet EF Core Tools"
$dotnetCommand = "dotnet"
$global:dotnetVersion = ""

# Check if dotnet is available
function Test-Dotnet {
    try {
        $global:dotnetVersion = & $dotnetCommand --version 2>&1
        Write-Info("Dotnet SDK Version is available: $global:dotnetVersion")
        return $true
    } catch {
        $errorMessage = $_.Exception.Message
        Write-Error("Dotnet SDK is not available. Info: $errorMessage")
        return $false
    }
}

# Check if 'dotnet ef' is available
function Test-EfCoreTooling() {
    if (Test-Dotnet) {
        try {
            $efCommandOutput = & $dotnetCommand ef

            if($efCommandOutput.Length -eq 0)
            {
                throw "$toolsName are not available."
            }

            Write-Info("$toolsName are available.")
            return $true
        } catch {
            $errorMessage = $_.Exception.Message
            Write-Error($errorMessage)
            return $false
        }
    } else {
        throw "Dotnet Core is not available. We need the more than 6 version of Dotnet Core SDK."
    }
}

# Install EF tools if 'dotnet ef' is not available
if (-not (Test-EfCoreTooling)) {
    Write-Info("I have to install $toolsName. Are you sure you want to continue? (y/n)")
    
    $answer = Read-Host
    
    if($answer -eq "y")
    {
        Write-Info("Installing $toolsName...")

        dotnet tool install --global dotnet-ef --version $dotnetVersion.Substring(0, 1)
     
        Write-Info("$toolsName have been installed.")
        
        if(-not (Test-EfCoreTooling))
        {
            exit
        }
    }
    else {
        Write-Info("$toolsName are not installed. Exiting...")
        exit
    }
}

# Define a custom object with fields
$migration = New-Object -TypeName PSObject -Property @{
    DbContextProject = ".\..\..\..\EFCoreMigration\EFCoreTest\EFCoreTest.csproj"
    EF6MigrationsPath = ".\Gizmo.DAL.EF6.Migrations.MSSQL\"
    EFCoreMigrationsPath = ".\Gizmo.DAL.EFCore.Migrations.MSSQL"
    InitMigrationName = "20231113182556_Initial"
}

Write-Info("Getting EF6 init migration")

$ef6InitMigration = dotnet ef migrations list `
    --project $migration.EF6MigrationsPath `
    --startup-project $migration.DbContextProject `
    -- output string | Select-Object `
    -Last 1

Write-Info("EF6 init migration:`n$ef6InitMigration")

# Write-Info("Getting EFCore migrations")

# $efCoreMigrations = dotnet ef migrations list `
#     --project $migration.EFCoreMigrationsPath `
#     --startup-project $migration.DbContextProject `
#     --output json | ConvertFrom-Json | Select-Object -Last 1

# foreach ($efCoreMigration in $efCoreMigrations) {
#     Write-Info("EFCore migration: $efCoreMigration")
# }


# # Revert migrations
# dotnet ef database update $initMigration
    
# while ($currentMigration -ne $initMigration) {
#     dotnet ef migrations remove --project .\YourDbContextProject.csproj --startup-project .\YourStartupProject.csproj
#     $currentMigration = dotnet ef migrations list --project .\YourDbContextProject.csproj --startup-project .\YourStartupProject.csproj --output json | ConvertFrom-Json | Select-Object -Last 1
# }

# Write-Info("All migrations have been reverted to $initMigration"