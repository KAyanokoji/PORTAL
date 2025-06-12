using Microsoft.AspNetCore.Routing;
using PORTAL.APPLICATION.Services;
using PORTAL.INFRASTRUCTURE.Configurations.Logging;
using PORTAL.SHARED;
using PORTAL.SHARED.Utils;

namespace PORTAL.API.Routes;

public static class AuthRoutes
{

    public static void RegisterAuthRoutes(this WebApplication app)
    {
        app.MapPost("/auth/login", Login);
        app.MapPost("/auth/registration", Registration);
    }

    private static async Task<IResult> Registration(Register register, IAuthService authService, IGlobalLogger logger)
    {
        try
        {
            await authService.Register(register);
            return Results.Created();
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while registering user {register.Email}", ex);
            throw;
        }
    }

    private static async Task<IResult> Login(Login request, IAuthService authService, IGlobalLogger logger)
    {
        try
        {
            var data = await authService.Authenticate(request);
            var response = ApiResponse.AuthenticationSuccess(data);
            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while registering user {request.Username}", ex);
            throw;
        }
    }
}