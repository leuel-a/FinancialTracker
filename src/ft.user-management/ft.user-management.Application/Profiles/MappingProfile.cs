using AutoMapper;
using ft.user_management.Domain.Entities;
using ft.user_management.Application.Dtos.Users;

namespace ft.user_management.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, CreateUserDto>().ReverseMap();
    }
}