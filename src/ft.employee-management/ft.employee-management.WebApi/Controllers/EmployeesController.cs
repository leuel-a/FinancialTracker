using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ft.employee_management.Application.Dtos.EmployeeDto;
using ft.employee_management.Application.Features.Employee.Requests.Queries;
using ft.employee_management.Application.Features.Employee.Requests.Commands;

namespace ft.employee_management.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _mediator.Send(new GetAllEmployeesRequest());
        return Ok(employees);
    }

    [HttpGet("{id:int}", Name = "GetEmployeeById")]
    public Task<IActionResult> GetEmployeeById(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewEmployee([FromBody] CreateEmployeeDto employee)
    {
        var result = await _mediator.Send(new CreateEmployeeCommand { EmployeeDto = employee });
        return CreatedAtRoute("GetEmployeeById", new { id = result.Id }, result);
    }
}