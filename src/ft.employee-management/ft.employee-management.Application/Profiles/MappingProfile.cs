using AutoMapper;
using ft.employee_management.Domain.Entities;
using ft.employee_management.Application.Dtos.Employee;

namespace ft.employee_management.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Employee mappings, might need to be updated
        CreateMap<Employee, ReadEmployeeDto>().ReverseMap();
        CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
    }
}