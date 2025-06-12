// PORTAL.APPLICATION/DTOs/Profiles/PermissionProfile.cs
using AutoMapper;
using PORTAL.APPLICATION.DTOs.request;
using PORTAL.APPLICATION.DTOs.Responses;
using PORTAL.DOMAIN.Entities;

namespace PORTAL.APPLICATION.DTOs.Profiles;

public class PermissionProfile : Profile
{
    public PermissionProfile()
    {
        // Request DTO → Domain Entity
        CreateMap<PermissionCreateDto, Permission>();

        // Domain Entity → Response DTO
        CreateMap<Permission, PermissionResponseDto>();
    }
}