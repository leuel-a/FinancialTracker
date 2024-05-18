using System;
using AutoMapper;
using ft.user_management.Domain.Entities;
using ft.user_management.Application.Dtos.Role;
using ft.user_management.Application.Dtos.User;

namespace ft.user_management.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, ApplicationUser>().ForMember(p => p.DateOfBirth,
            opt => opt.MapFrom(src => DateTime.Parse(src.DateOfBirth!)));
        CreateMap<ApplicationUser, CreateUserDto>().ForMember(dest => dest.DateOfBirth,
            opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()));

        CreateMap<ApplicationUser, ReadUserDto>().ForMember(dest => dest.DateOfBirth,
            options => options.MapFrom(src => src.DateOfBirth.ToString()));

        CreateMap<ApplicationRole, RoleDto>().ReverseMap();
        CreateMap<ApplicationRole, UpdateRoleDto>().ReverseMap();
        CreateMap<ApplicationRole, CreateRoleDto>().ReverseMap();
    }
}