using AutoMapper;
using Core.Entities.Concretes;
using Core.Entities.Dtos.Auth;
using Core.Entities.Dtos.User;
using Core.Extensions.Paging;

namespace Business.MappingProfiles;

public sealed class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, RegisterDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<User, UserDeleteDto>().ReverseMap();
        CreateMap<User, UserGetDto>().ReverseMap();
        CreateMap<PageableModel<User>, PageableModel<UserGetDto>>().ReverseMap();
    }
}