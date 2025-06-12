<#
.SYNOPSIS
    Starts the PORTAL solution .NET projects.
.DESCRIPTION
    This script starts the PORTAL.API project (and optionally other projects) with proper configuration.
    It handles environment settings, dependency checks, and can run in watch mode.
.PARAMETER Environment
    The environment to run in (Development, Staging, Production - defaults to Development)
.PARAMETER Watch
    Run using dotnet watch for hot reload
.PARAMETER NoBuild
    Skip the build step before running
.PARAMETER Migrate
    Run database migrations before starting
.EXAMPLE
    ./start-portal.ps1
.EXAMPLE
    ./start-portal.ps1 -Environment Staging -Watch
.EXAMPLE
    ./start-portal.ps1 -Migrate -NoBuild
.NOTES
    Designed for PORTAL solution structure:
    - PORTAL.API is the startup project
    - PORTAL.INFRASTRUCTURE contains DbContext
#>

param(
    [ValidateSet("Development", "Staging", "Production")]
    [string]$Environment = "Development",
    
    [switch]$Watch,
    [switch]$NoBuild,
    [switch]$Migrate
)

# Get the current script directory
$scriptPath = $PSScriptRoot
if (-not $scriptPath) {
    $scriptPath = Split-Path -Parent -Path $MyInvocation.MyCommand.Definition
}

# Set solution paths
$apiProject = Join-Path -Path $scriptPath -ChildPath "PORTAL.API/PORTAL.API.csproj"
$infrastructureProject = Join-Path -Path $scriptPath -ChildPath "PORTAL.INFRASTRUCTURE/PORTAL.INFRASTRUCTURE.csproj"

# Verify projects exist
if (-not (Test-Path $apiProject)) {
    Write-Host "Error: PORTAL.API project not found at $apiProject" -ForegroundColor Red
    exit 1
}

# Display startup information
Write-Host "Starting PORTAL solution..." -ForegroundColor Cyan
Write-Host "Environment: $Environment"
Write-Host "API Project: $apiProject"
Write-Host "Watch Mode: $($Watch.IsPresent)"
Write-Host "Run Migrations: $($Migrate.IsPresent)"
Write-Host ""

# Set environment variables
$env:ASPNETCORE_ENVIRONMENT = $Environment

try {
    # Optionally run migrations
    if ($Migrate) {
        Write-Host "Running database migrations..." -ForegroundColor Yellow
        $migrateCommand = "dotnet ef database update --project `"$infrastructureProject`" --startup-project `"$apiProject`""
        Invoke-Expression $migrateCommand
        
        if ($LASTEXITCODE -ne 0) {
            throw "Database migration failed!"
        }
        Write-Host "Migrations applied successfully." -ForegroundColor Green
    }

    # Build the project (unless skipped)
    if (-not $NoBuild) {
        Write-Host "Building solution..." -ForegroundColor Yellow
        dotnet build $apiProject
        if ($LASTEXITCODE -ne 0) {
            throw "Build failed!"
        }
    }

    # Run the project
    Write-Host "Starting PORTAL.API..." -ForegroundColor Green
    
    if ($Watch) {
        Write-Host "Running in watch mode (hot reload enabled)" -ForegroundColor Magenta
        dotnet watch run --project $apiProject
    }
    else {
        dotnet run --project $apiProject
    }
}
catch {
    Write-Host "ERROR: $_" -ForegroundColor Red
    exit 1
}