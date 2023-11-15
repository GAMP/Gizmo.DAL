# Define a migration data
$migration = New-Object -TypeName PSObject -Property @{
    DbContextProject = ".\..\..\..\EFCoreMigration\EFCoreTest\EFCoreTest.csproj"
    EF6MigrationsPath = ".\Gizmo.DAL.EF6.Migrations.MSSQL\"
    EFCoreMigrationsPath = ".\Gizmo.DAL.EFCore.Migrations.MSSQL"
    InitMigrationName = "20231115164653_Initial"
}

$toolsName = "Dotnet EF Core Tools"
$dotnetCommand = "dotnet"
$global:dotnetVersion = ""

function Write-Info($message){
    Write-Host "`n$message" -ForegroundColor Yellow
}
function Write-Success($message){
    Write-Host "`n$message" -ForegroundColor Green
}
function Write-Error($message){
    Write-Host "`n$message" -ForegroundColor Red
}
# Check if dotnet is available
function Test-Dotnet {
    try {
        $global:dotnetVersion = & $dotnetCommand --version
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
        throw ".Net Core SDK is not available. The 6.+ version is required."
    }
}
# Install EF tools if 'dotnet ef' is not available
if (-not (Test-EfCoreTooling)) {
    Write-Info("I have to install $toolsName. Are you sure you want to continue? (y/n)")
    
    $answer = Read-Host
    
    if($answer -eq "y", "Y")
    {
        Write-Info("Installing $toolsName...")

        dotnet tool install --global dotnet-ef --version $dotnetVersion.Substring(0, 1)
     
        if(-not (Test-EfCoreTooling))
        {
            exit
        }

        Write-Info("$toolsName have been installed.")
    }
    else {
        Write-Info("$toolsName are not installed. Exiting...")
        exit
    }
}

Write-Info("Getting an Initial migration from the database.")

# Get the first migration
$migrations = dotnet ef migrations list `
    --startup-project $migration.DbContextProject `
    --project $migration.EFCoreMigrationsPath

$migrationPattern = "\d{14}_\w+"

$migrations = $migrations -match $migrationPattern

# Check if any migrations were retrieved successfully
if ($null -eq $migrations) {
    Write-Error("No migrations were found in the database that match the pattern. Exiting...")
    exit
}

$dbInitMigration = $migrations | Where-Object { $_ -eq $migration.InitMigrationName } | Select-Object -First 1
    
# Check if the migration was retrieved successfully
if ($null -eq $dbInitMigration) {
    Write-Error("Initial migration was not found in the database. Exiting...")
    exit
}

Write-Success("Initial migration was found: $dbInitMigration.")

if($migrations.Length -eq 1)
{
    Write-Info("There is only Initial migration in the database. Exiting...")
    exit
}

Write-Info("Rolling back the database to {0}.`n" -f $migration.InitMigrationName)

# Set the initial migration
dotnet ef database update $migration.InitMigrationName `
    --startup-project $migration.DbContextProject `
    --project $migration.EF6MigrationsPath

Write-Success("Initial migration has been set to {0}." -f $migration.InitMigrationName)

Write-Info("Removing all unapplied migrations from the database.")

# Remove migrations
dotnet ef migrations remove `
    --startup-project $migration.DbContextProject `
    --project $migration.EFCoreMigrationsPath `
    --force

Write-Success("All unapplied migrations have been removed.")
