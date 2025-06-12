using PORTAL.APPLICATION.DTOs.request;
using PORTAL.APPLICATION.Interfaces;
using PORTAL.INFRASTRUCTURE.Configurations.Logging;

namespace PORTAL.API.Routes;

public static class PermissionRoute
{
    public static void RegisterPermissionRoutes(this WebApplication app)
    {
        app.MapPost("/Permission", Create);
        app.MapGet("/Permission", GetAllPermissions);
        app.MapGet("/Permission/{PermissionId}", GetPermissionById);
        app.MapPut("/Permission/{PermissionId}", UpdatePermission);
        app.MapDelete("/permission/{permissionId}", DeletePermission);
    }

    private static async Task<IResult> Create(PermissionCreateDto request, IGlobalLogger logger, IPermissionService services)
    {
        try
        {
            await services.Create(request);
            return Results.Created();
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while registering user {request.Description}", ex);
            throw;
        }
    }

    private static async Task<IResult> GetAllPermissions(IGlobalLogger logger, IPermissionService services)
    {
        try
        {
            var permissions = await services.GetAll();
            return Results.Ok(permissions);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while registering user ", ex);
            throw;
        }
    }
    public static async Task<IResult> GetPermissionById(IGlobalLogger logger, IPermissionService service, int PermissionId)
    {
        try
        {
            var permission = await service.GetById(PermissionId);
            return Results.Ok(permission);
        }
        catch (Exception ex)
        {
            logger.LogError("Error while fetching permission with ID {PermissionId}", ex);
            throw;
        }
    }

    public static async Task<IResult> UpdatePermission(IGlobalLogger logger, IPermissionService service, PermissionUpdateDto update, int PermissionId)
    {
        try
        {
            var permission = await service.UpdatePermission(update, PermissionId);
            return Results.Ok(permission);
        }
        catch (Exception ex)
        {
            logger.LogError("error while update Permission", ex);
            throw;
        }
    }

    public static async Task<IResult> DeletePermission(IGlobalLogger logger, IPermissionService service, int PermissionId) {
        try
        {
            var permission = await service.DeletePermission( PermissionId);
            return Results.Ok(permission);
        }
        catch (Exception ex)
        {
            logger.LogError("error while update Permission", ex);
            throw;
        }
    }
}
