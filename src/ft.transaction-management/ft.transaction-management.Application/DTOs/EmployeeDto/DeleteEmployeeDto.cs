using System;
using ft.transaction_management.Application.DTOs.CommonDto;

namespace ft.transaction_management.Application.DTOs.EmployeeDto;

public class DeleteEmployeeDto : BaseDto
{
    public Guid Id { get; set; }
}