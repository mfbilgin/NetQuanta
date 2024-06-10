using AutoMapper;
using Core.Entities.Concretes;
using Core.Entities.Dtos.Role;
using Core.Extensions.Paging;

namespace Business.MappingProfiles;

public sealed class RoleMapper : Profile
{
    public RoleMapper()
    {
        CreateMap<Role, RoleAddDto>().ReverseMap();
        CreateMap<Role, RoleUpdateDto>().ReverseMap();
        CreateMap<Role, RoleDeleteDto>().ReverseMap();
        CreateMap<Role, RoleGetDto>().ReverseMap();
        CreateMap<PageableModel<Role>, PageableModel<RoleGetDto>>().ReverseMap();
    }
}