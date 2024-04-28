using ft.employee_management.Application.Dtos.EmployeeDto;
using MediatR;

namespace ft.employee_management.Application.Features.Employee.Requests.Queries;

public class GetEmployeeByIdRequest : IRequest<EmployeeDto>
{
    public int Id { get; set; }
}