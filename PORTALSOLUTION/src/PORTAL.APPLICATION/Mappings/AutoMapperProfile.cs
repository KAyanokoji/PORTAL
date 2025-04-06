using AutoMapper;
using PORTAL.DOMAIN.Entities;
using PORTAL.SHARED;

namespace PORTAL.APPLICATION.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Register, User>();
    }
}