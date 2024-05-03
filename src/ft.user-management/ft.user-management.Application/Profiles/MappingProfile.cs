using AutoMapper;
using ft.user_management.Application.Dtos.Users;
using ft.user_management.Domain.Entities;

namespace ft.user_management.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, CreateUserDto>().ReverseMap();
    }
}