using AutoMapper;
using ft.user_management.Domain.Entites;
using ft.user_management.Application.Dtos.User;

namespace ft.user_management.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, User>().ReverseMap();
    }
}