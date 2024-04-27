using AutoMapper;
using ft.employee_management.Application.Dtos.EmployeeDto;
using ft.employee_management.Application.Dtos.EmployeeTypeDto;
using ft.employee_management.Domain.Entities;

namespace ft.employee_management.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
        CreateMap<Employee, ReadEmployeeDto>().ReverseMap();

        CreateMap<EmployeeType, ReadEmployeeTypeDto>().ReverseMap();
    }
}