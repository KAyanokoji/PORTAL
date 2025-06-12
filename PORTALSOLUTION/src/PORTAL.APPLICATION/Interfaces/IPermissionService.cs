using PORTAL.APPLICATION.DTOs.request;
using PORTAL.SHARED.Utils;

namespace PORTAL.APPLICATION.Interfaces;

public interface IPermissionService
{
    Task<JsonResponse> GetAll();
    Task<JsonResponse> GetById(int id);
    Task<JsonResponse> Create(PermissionCreateDto permission);
    Task<JsonResponse> UpdatePermission(PermissionUpdateDto permission, int PermissionId);
    Task<JsonResponse> DeletePermission(int PermissionId);

}
