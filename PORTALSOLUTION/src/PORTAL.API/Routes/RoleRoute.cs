using PORTAL.APPLICATION.DTOs.request;
using PORTAL.APPLICATION.Interfaces;
using PORTAL.INFRASTRUCTURE.Configurations.Logging;

namespace PORTAL.API.Routes;


public static class RoleRoute
{
    public static void RegisterRoleRoutes(this WebApplication app)
    {
        app.MapGet("/roles", GetRoles);
        app.MapGet("/roles/{roleId}", GetRoleById);
        app.MapPost("/roles", CreateRole);
        app.MapPut("/roles/{roleId}", UpdateRole);
        app.MapDelete("/roles/{roleId}", DeleteRole);
    }

    private static async Task<IResult> GetRoles(IGlobalLogger logger, IRoleService service)
    {
        try
        {
            var roles = await service.GetRole();
            return Results.Ok(roles);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while fetching roles ", ex);
            throw;
        }
    }

    private static async Task<IResult> GetRoleById(IGlobalLogger logger, IRoleService service, int roleId)
    {
        try
        {
            var role = await service.GetRoleById(roleId);
            return Results.Ok(role);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while getting role", ex);
            throw;
        }
    }

    private static async Task<IResult> CreateRole(IGlobalLogger logger, IRoleService service, RoleCreateDto request)
    {
        try
        {
            await service.CreateRole(request);
            return Results.Created();
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occor while create role.", ex);
            throw;
        }
    }

    private static async Task<IResult> UpdateRole(IGlobalLogger logger, IRoleService service, RoleUpdateDto request, int roleId)
    {
        try
        {
            var update = await service.UpdateRole(request, roleId);
            return Results.Ok(update);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while update role", ex);
            throw;
        }
    }

    private static async Task<IResult> DeleteRole(IGlobalLogger logger, IRoleService service, int roleId)
    {
        try
        {
            var delete = await service.DeleteRole(roleId);
            return Results.Ok(delete);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while deleting role", ex);
            throw;
        }
    }
}