<#
.SYNOPSIS
    Removes the most recent EF Core migration for PORTAL solution.
.DESCRIPTION
    This script removes the most recent migration and reverts the database.
    Uses the correct approach for current EF Core versions.
.PARAMETER Context
    The DbContext to use (default: ApplicationDbContext)
.PARAMETER ConnectionString
    The database connection string to use (optional)
.EXAMPLE
    ./ef-remove.ps1
.EXAMPLE
    ./ef-remove.ps1 -ConnectionString "Host=localhost;Port=5432;Database=portal;Username=postgres;Password=yourpassword"
.NOTES
    This removes the MOST RECENT migration only.
    For older EF Core versions, you might need to specify a target migration.
#>

param(
    [string]$Context = "ApplicationDbContext",

    [string]$ConnectionStringName = "PostgresSQLConnection",

    
    [string]$ConnectionString 
)

# Get the current script directory
$scriptPath = $PSScriptRoot
if (-not $scriptPath) {
    $scriptPath = Split-Path -Parent -Path $MyInvocation.MyCommand.Definition
}

# Set absolute paths
$infrastructureProject = Join-Path -Path $scriptPath -ChildPath "PORTAL.INFRASTRUCTURE"
$apiProject = Join-Path -Path $scriptPath -ChildPath "PORTAL.API"

# Verify projects exist
if (-not (Test-Path $infrastructureProject)) {
    Write-Host "Error: PORTAL.INFRASTRUCTURE project not found at $infrastructureProject" -ForegroundColor Red
    exit 1
}

if (-not (Test-Path $apiProject)) {
    Write-Host "Error: PORTAL.API project not found at $apiProject" -ForegroundColor Red
    exit 1
}

# Display information
Write-Host "Starting migration removal process..." -ForegroundColor Cyan
Write-Host "DbContext: $Context"
Write-Host "Infrastructure project: $infrastructureProject"
Write-Host "API project (startup): $apiProject"
if ($ConnectionString) {
    Write-Host "Using explicit connection string"
} else {
    Write-Host "Using connection string from appsettings.json"
}
Write-Host ""

# Build common command parameters
$commonParams = "--project `"$infrastructureProject`" --startup-project `"$apiProject`" --context $Context"

# Step 1: Get the current migration list
Write-Host "Getting migration history..." -ForegroundColor Yellow
$migrationsCommand = "dotnet ef migrations list $commonParams"
$migrations = Invoke-Expression $migrationsCommand | Where-Object { $_ -match '^\w' }

if (-not $migrations) {
    Write-Host "No migrations found to remove!" -ForegroundColor Red
    exit 1
}

$latestMigration = $migrations[-1]
Write-Host "Latest migration: $latestMigration" -ForegroundColor Yellow

# Step 2: Revert the database (using update to previous migration)
if ($migrations.Count -gt 1) {
    $targetMigration = $migrations[-2]
    Write-Host "Reverting database to: $targetMigration..." -ForegroundColor Yellow
    $revertParams = $commonParams
    if ($ConnectionString) {
        $revertParams += " --connection `"$ConnectionString`""
    }
    $revertCommand = "dotnet ef database update $targetMigration $revertParams"
    Write-Host "Executing: $revertCommand"
    Invoke-Expression $revertCommand

    if ($LASTEXITCODE -ne 0) {
        Write-Host "Database revert failed!" -ForegroundColor Red
        exit $LASTEXITCODE
    }
} else {
    Write-Host "This is the first migration - will only remove files without database changes" -ForegroundColor Yellow
}

# Step 3: Remove the migration
Write-Host "Removing migration '$latestMigration'..." -ForegroundColor Yellow
$removeCommand = "dotnet ef migrations remove $commonParams"
Write-Host "Executing: $removeCommand"
Invoke-Expression $removeCommand

if ($LASTEXITCODE -ne 0) {
    Write-Host "Migration removal failed!" -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "Migration '$latestMigration' removed successfully." -ForegroundColor Green
Write-Host "Migration removal process completed." -ForegroundColor Cyan