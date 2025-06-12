using PORTAL.APPLICATION.DTOs.request;
using PORTAL.SHARED.Utils;

namespace PORTAL.APPLICATION.Interfaces;

public interface IRoleService
{
    Task<JsonResponse> GetRole();
    Task<JsonResponse> CreateRole(RoleCreateDto request);
    Task<JsonResponse> GetRoleById(int roleId);
    Task<JsonResponse> UpdateRole(RoleUpdateDto request, int roleId);
    Task<JsonResponse> DeleteRole(int roleId);
}