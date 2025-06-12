<#
.SYNOPSIS
    Creates and applies EF Core migrations in a single step for PORTAL solution.
.DESCRIPTION
    This script creates a new EF Core migration with a specified name and immediately applies it to the database.
    Specifically designed for the PORTAL solution structure with DbContext in INFRASTRUCTURE and API as startup.
.PARAMETER MigrationName
    The name to give to the new migration (required)
.PARAMETER Context
    The DbContext to use (default: ApplicationDbContext)
.PARAMETER ConnectionString
    The database connection string to use (optional if set in appsettings.json)
.EXAMPLE
    ./ef-migrate.ps1 -MigrationName "AddUserTable"
.EXAMPLE
    ./ef-migrate.ps1 -MigrationName "AddUserTable" -ConnectionString  "Server = localhost; Port = 5432; Database = portal; User Id = postgres; Password = g_38@pgsql;"
.NOTES
    Requires .NET Core SDK and Entity Framework Core tools to be installed.
    Designed for PORTAL solution structure:
    - PORTAL.INFRASTRUCTURE contains DbContext (ApplicationDbContext)
    - PORTAL.API is the startup project
    - Migrations are in PORTAL.INFRASTRUCTURE/Persistence/Migrations
#>

param(
    [Parameter(Mandatory=$true)]
    [string]$MigrationName,
    
    [string]$Context = "ApplicationDbContext",

    [string]$ConnectionStringName = "PostgresSQLConnection",

    
    [string]$ConnectionString 
)

# Get the current script directory
$scriptPath = $PSScriptRoot
if (-not $scriptPath) {
    $scriptPath = Split-Path -Parent -Path $MyInvocation.MyCommand.Definition
}

# Set absolute paths based on your solution structure
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

# Display starting information
Write-Host "Starting EF Core migration process for PORTAL solution..." -ForegroundColor Cyan
Write-Host "Migration name: $MigrationName"
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

# Step 1: Create the migration
Write-Host "Creating migration '$MigrationName'..." -ForegroundColor Yellow
$createCommand = "dotnet ef migrations add $MigrationName $commonParams --output-dir Persistence/Migrations"
Write-Host "Executing: $createCommand"
Invoke-Expression $createCommand

if ($LASTEXITCODE -ne 0) {
    Write-Host "Migration creation failed!" -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "Migration created successfully in PORTAL.INFRASTRUCTURE/Persistence/Migrations." -ForegroundColor Green

# Step 2: Apply the migration to the database
Write-Host "Applying migration to database..." -ForegroundColor Yellow
$updateParams = $commonParams
if ($ConnectionString) {
    $updateParams += " --connection `"$ConnectionString`""
}
$updateCommand = "dotnet ef database update $updateParams"
Write-Host "Executing: $updateCommand"
Invoke-Expression $updateCommand

if ($LASTEXITCODE -ne 0) {
    Write-Host "Database update failed!" -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "Database updated successfully." -ForegroundColor Green
Write-Host "Migration process completed." -ForegroundColor Cyan