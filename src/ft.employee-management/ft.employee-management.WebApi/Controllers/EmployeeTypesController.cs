using System;
using System.Threading.Tasks;
using ft.employee_management.Application.Dtos.EmployeeTypeDto;
using ft.employee_management.Application.Features.EmployeeType.Requests.Commands;
using ft.employee_management.Application.Features.EmployeeType.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ft.employee_management.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployeeTypes()
    {
        var employeeTypes = await _mediator.Send(new GetAllEmployeeTypesRequest());
        return Ok(employeeTypes);
    }

    [HttpGet("{id:int}", Name = "GetEmployeeTypeById")]
    public async Task<IActionResult> GetEmployeeTypeById(int id)
    {
        var employeeType = await _mediator.Send(new GetEmployeeTypeRequest() { Id = id });
        return Ok(employeeType);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployeeType([FromBody] CreateEmployeeTypeDto employeeTypeDto)
    {
        var employeeType = await _mediator.Send(new CreateEmployeeTypeCommand()
            { CreateEmployeeTypeDto = employeeTypeDto });
        return CreatedAtRoute("GetEmployeeTypeById", new { Id = employeeType.Id }, employeeType);
    }
}