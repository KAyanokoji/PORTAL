using System.Net;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PORTAL.APPLICATION.DTOs.request;
using PORTAL.APPLICATION.DTOs.Responses;
using PORTAL.APPLICATION.Interfaces;
using PORTAL.DOMAIN.Entities;
using PORTAL.DOMAIN.Exceptions;
using PORTAL.DOMAIN.Interfaces;
using PORTAL.SHARED.Utils;

namespace PORTAL.APPLICATION.Services;

public class RoleServices(IUnitOfWork unit, IMapper mapper, ILogger<RoleServices> logger) : IRoleService
{
    private readonly IUnitOfWork unit = unit;
    private readonly IMapper mapper = mapper;
    private readonly ILogger logger = logger;

    public async Task<JsonResponse> CreateRole(RoleCreateDto request)
    {
        var response = new JsonResponse();
        try
        {
            var isRole = await unit.Roles.FindAsync(role => role.RoleName == request.RoleName);
            if (isRole != null) throw new DuplicateDataException("Role", request.RoleName);
            var role = mapper.Map<Role>(request);
            role.CreatedBy = "Admin";

            await unit.Roles.AddAsync(role);
            await unit.CompleteAsync();

            response.IsSuccess = true;
            response.StatusCode = (int)HttpStatusCode.Created;
            response.Message = "Role Created Successfully";

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating Role");
            response.IsSuccess = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Message = "An error occurred while creating permission";

            throw;
        }
        return response;
    }

    public async Task<JsonResponse> DeleteRole(int roleId)
    {
        var response = new JsonResponse();
        try
        {
            var roleDb = await unit.Roles.GetByIdAsync(roleId) ?? throw new EntityNotFoundException("Role Delete", roleId);
            if (roleDb is ISoftDeletable deletable)
            {
                deletable.IsDeleted = true;
                await unit.Roles.UpdateAsync(roleDb);
            }

            response.IsSuccess = true;
            response.Message = "Role Deleted successfully";
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while Updating Role with ID {roleId}", roleId);

            response.IsSuccess = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Message = "An error occurred while Updating the Role";
            throw;
        }
        return response;
    }

    public async Task<JsonResponse> GetRole()
    {
        var response = new JsonResponse();
        try
        {
            var roles = await unit.Roles.GetAllAsync();
            response.IsSuccess = true;
            response.StatusCode = (int)HttpStatusCode.OK;
            response.ResponseData = mapper.Map<IEnumerable<RoleResponseDto>>(roles);
            response.Message = roles.Any() ? "Roles fetched successfully" : "No roles found";
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while fetching all Roles");
            response.IsSuccess = false;
            response.Message = "An error occurred while fetching roles";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            throw;
        }
        return response;
    }

    public async Task<JsonResponse> GetRoleById(int roleId)
    {
        var response = new JsonResponse();
        try
        {
            if (roleId < 0) throw new DataIntegrityViolationException("Invalid RoleId");
            var role = await unit.Roles.GetByIdAsync(roleId) ?? throw new EntityNotFoundException("Role", roleId);
            response.IsSuccess = true;
            response.StatusCode = (int)HttpStatusCode.OK;
            response.ResponseData = mapper.Map<RoleResponseDto>(role);
            response.Message = "Roles fetched successfully";

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while fetching role  id {roleId}", roleId);
            throw;
        }
        return response;
    }

    // public async Task<JsonResponse> GetRoleById(int roleId, string message)
    // {
    //     var response = new JsonResponse();
    //     try
    //     {
    //         var role = await repository.GetRoleById(roleId);
    //         response.IsSuccess = true;
    //         response.StatusCode = (int)HttpStatusCode.OK;
    //         response.ResponseData = mapper.Map<RoleResponseDto>(role);
    //         response.Message = "Roles fetched successfully";

    //     }
    //     catch (Exception ex)
    //     {

    //         logger.LogError(ex, message: message, roleId);
    //         response.IsSuccess = false;
    //         response.Message = "An error occurred while fetching role  id {roleId}";
    //         response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //         throw;
    //     }
    //     return response;
    // }

    public async Task<JsonResponse> UpdateRole(RoleUpdateDto request, int roleId)
    {
        var response = new JsonResponse();
        try
        {
            if (request.RoleId != roleId)
                throw new EntityNotFoundException("Update role", roleId);
            var roleDb = await unit.Roles.GetByIdAsync(roleId) ?? throw new EntityNotFoundException("Role UPdate", roleId);
            roleDb.RoleName = request.RoleName;
            roleDb.Description = request.Description;
            roleDb.IsActive = request.IsActive;
            await unit.Roles.UpdateAsync(roleDb);

            response.IsSuccess = true;
            response.Message = "Permission updated successfully";
            response.ResponseData = mapper.Map<RoleResponseDto>(roleDb);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while Updating role with ID {roleId}", roleId);

            response.IsSuccess = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Message = "An error occurred while Updating the role";
            throw;
        }
        return response;
    }
}