using AutoMapper;
using ft.employee_management.Domain.Entities;
using ft.employee_management.Application.Dtos.Employee;
using ft.employee_management.Domain.Enums;

namespace ft.employee_management.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Employee mappings, might need to be updated
        CreateMap<Employee, ReadEmployeeDto>()
            .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.EmployeeType.ToString())).ReverseMap();

        CreateMap<Employee, CreateEmployeeDto>()
            .ForMember(p => p.Type, opt => opt.MapFrom(src => src.EmployeeType.ToString()));

        CreateMap<CreateEmployeeDto, Employee>().ForMember(dest => dest.EmployeeType, opt =>
        {
            EmployeeType employeeType;
            opt.MapFrom(src =>
                Enum.TryParse(src.Type, true, out employeeType)
                    ? employeeType
                    : EmployeeType.FullTime);
        });
    }
}