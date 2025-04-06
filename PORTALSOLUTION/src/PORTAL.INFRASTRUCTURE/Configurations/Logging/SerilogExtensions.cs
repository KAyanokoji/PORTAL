using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace PORTAL.INFRASTRUCTURE.Configurations.Logging;

public static class SerilogExtensions
{
    //public static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
    //{
    //    // read sherilog settings from appsetting.json
    //    var serilogsettings = new SerilogSettings();
    //    configuration.GetSection("Serilog").Bind(serilogsettings);

    //    // Configure serilog

    //    Log.Logger = new LoggerConfiguration()
    //        .MinimumLevel.Information()
    //        .Enrich.FromLogContext()
    //        .WriteTo.Console()
    //        .WriteTo.File(serilogsettings.LogFilePath, rollingInterval: RollingInterval.Day)
    //        .CreateLogger();
    //    // Register Serilog
    //    services.AddLogging(loggingBuilder =>
    //    {
    //        loggingBuilder.ClearProviders();
    //        loggingBuilder.AddSerilog(dispose: true);
    //    });

    //    return services;
    //}

    public static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
    {
        // Read Serilog settings from appsettings.json
        var serilogsettings = new SerilogSettings();
        configuration.GetSection("Serilog").Bind(serilogsettings);

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Is(Enum.Parse<LogEventLevel>(configuration["Logging:LogLevel:Default"])) // Reading from appsettings.json
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(serilogsettings.LogFilePath, rollingInterval: RollingInterval.Day)
            .CreateLogger();

        // Register Serilog
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders(); // Remove default logging providers
            loggingBuilder.AddSerilog(dispose: true); // Add Serilog
        });

        return services;
    }
}