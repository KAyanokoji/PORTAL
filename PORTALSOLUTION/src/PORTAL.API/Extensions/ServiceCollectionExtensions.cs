using Microsoft.EntityFrameworkCore;
using PORTAL.APPLICATION.Mappings;
using PORTAL.APPLICATION.Services;
using PORTAL.DOMAIN.Interfaces;
using PORTAL.INFRASTRUCTURE.Configurations.Logging;
using PORTAL.INFRASTRUCTURE.Persistence;
using PORTAL.INFRASTRUCTURE.Repositories;
using PORTAL.INFRASTRUCTURE.Services;
using System.Text.Json.Serialization;

namespace PORTAL.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAutoMapper(typeof(AutoMapperProfile)); // Register your AutoMapper profile

        services.AddSerilogLogging(configuration);
        services.AddSingleton<IGlobalLogger, GlobalLogger>(); // Register the global logger

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresSQLConnection")));
        services.AddTransient<IAuthService, AuthServices>();
        services.AddTransient<IAuthRepository,AuthRepo>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();


        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));

        return services;
    }
}