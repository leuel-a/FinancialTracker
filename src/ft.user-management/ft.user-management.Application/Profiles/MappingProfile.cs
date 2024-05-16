using AutoMapper;
using ft.user_management.Application.Dtos.User;
using ft.user_management.Domain.Entities;

namespace ft.user_management.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, ApplicationUser>().ReverseMap();
    }
}