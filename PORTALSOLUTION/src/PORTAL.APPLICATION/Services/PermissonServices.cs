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

public class PermissionService(IMapper mapper, ILogger<PermissionService> logger, IUnitOfWork unit) : IPermissionService
{
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<PermissionService> logger = logger;
    private readonly IUnitOfWork unit = unit;

    public async Task<JsonResponse> Create(PermissionCreateDto request)
    {
        var response = new JsonResponse();
        try
        {
            var existingPermission = await unit.Permissions.FindAsync(x => x.PermissionName == request.PermissionName);
            if (existingPermission.Any())
                throw new DuplicateDataException("Permission", request.PermissionName);
            var permission = _mapper.Map<Permission>(request);
            var getPermissionId = await unit.Permissions.GetLatestId<Permission>();
            permission.Id = getPermissionId + 1;
            permission.CreatedBy = "Admin";
            await unit.Permissions.AddAsync(permission);
            await unit.CompleteAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating permission");
            response.IsSuccess = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Message = "An error occurred while creating permission";
            throw;
        }
        return response;
    }

    public async Task<JsonResponse> GetAll()
    {
        var response = new JsonResponse();
        try
        {
            var permissions = await unit.Permissions.GetAllAsync();
            response.IsSuccess = true;
            response.ResponseData = _mapper.Map<IEnumerable<PermissionResponseDto>>(permissions);
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Message = permissions.Any() ? "Fetched Successfully" : "No Data Found";
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while fetching all permissions");
            response.IsSuccess = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Message = "An error occurred while fetching permissions";
        }
        return response;
    }

    public async Task<JsonResponse> GetById(int id)
    {
        var response = new JsonResponse();
        try
        {
            if (id <= 0) throw new DataIntegrityViolationException("Invalid permission id");
            var permission = await unit.Permissions.GetByIdAsync(id) ?? throw new EntityNotFoundException("Permission", id);
            response.IsSuccess = true;
            response.StatusCode = (int)HttpStatusCode.OK;
            response.ResponseData = _mapper.Map<PermissionResponseDto>(permission);
            response.Message = "Permission retrieved successfully";
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while fetching permission with ID {PermissionId}", id);
            throw;
        }
        return response;
    }

    public async Task<JsonResponse> UpdatePermission(PermissionUpdateDto permission, int PermissionId)
    {
        var response = new JsonResponse();
        try
        {
            if (permission.PermissionId != PermissionId)
                throw new EntityNotFoundException("Update Permission", PermissionId);
            var permissionDb = await unit.Permissions.GetByIdAsync(PermissionId) ?? throw new EntityNotFoundException("Permission UPdate", PermissionId);
            permissionDb.PermissionName = permission.PermissionName;
            permissionDb.Description = permission.Description;
            permissionDb.IsActive = permission.IsActive;
            // Save changes
            await unit.Permissions.UpdateAsync(permissionDb);

            // Set success response
            response.IsSuccess = true;
            response.Message = "Permission updated successfully";
            response.ResponseData = _mapper.Map<PermissionResponseDto>(permissionDb);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while Updating permission with ID {PermissionId}", PermissionId);

            response.IsSuccess = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Message = "An error occurred while Updating the permission";
            throw;
        }
        return response;
    }


    public async Task<JsonResponse> DeletePermission(int PermissionId)
    {
        var response = new JsonResponse();
        try
        {
            var permissionDb = await unit.Permissions.GetByIdAsync(PermissionId) ?? throw new EntityNotFoundException("Permission Delete", PermissionId);
            if (permissionDb is ISoftDeletable deletable)
            {
                deletable.IsDeleted = true;  // Or IsDeleted = true
                await unit.Permissions.UpdateAsync(permissionDb);
            }
            //     permissionDb.IsActive = false;
            //     // Save changes
            //     await unit.Permissions.UpdateAsync(permissionDb);

            //     // Set success response
            response.IsSuccess = true;
            response.Message = "Permission Deleted successfully";
            // response.ResponseData = _mapper.Map<PermissionResponseDto>(permissionDb);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while Updating permission with ID {PermissionId}", PermissionId);

            response.IsSuccess = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Message = "An error occurred while Updating the permission";
            throw;
        }
        return response;


    }

}