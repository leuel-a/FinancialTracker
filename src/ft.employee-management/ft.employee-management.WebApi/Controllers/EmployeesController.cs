using ft.employee_management.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ft.employee_management.Application.Dtos.Employee;
using ft.employee_management.Application.Features.Employee.Requests.Queries;
using ft.employee_management.Application.Features.Employee.Requests.Commands;

namespace ft.employee_management.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}", Name = "GetSingleEmployee")]
    public async Task<IActionResult> GetSingleEmployee(int id)
    {
        var response = await _mediator.Send(new GetSingleEmployeeQuery { Id = id });

        if (response.Succeeded == false)
            return BadRequest(new {response.Message, Errors = response.ErrorMessages});
        return Ok(response.Resource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployees([FromQuery] GetAllEmployeesDto? dto)
    {
        var response = await _mediator.Send(new GetAllEmployeesQuery { GetAllEmployeesDto = dto });
        
        if (response.Succeeded == false)
            return BadRequest(new { response.Message, Errors = response.ErrorMessages });
        
        var paginationMetadata = new
        {
            response.PageSize,
            response.CurrentPage,
            response.TotalPages,
            response.TotalRecords,
            response.NextPage,
            response.PreviousPage,
            Data = response.Resources
        };
        return Ok(paginationMetadata);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployee)
    {
        var response = await _mediator.Send(new CreateEmployeeCommand { CreateEmployeeDto = createEmployee });

        if (response.Succeeded == false)
            return BadRequest(new { response.Message, Errors = response.ErrorMessages });
        return CreatedAtRoute("GetSingleEmployee", new {Id = response.Id}, response.Resource);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto updateEmployee)
    {
        updateEmployee.Id = id;
        var response = await _mediator.Send(new UpdateEmployeeCommand { UpdateEmployeeDto = updateEmployee });

        if (response.Succeeded == false)
            return BadRequest(new { response.Message, Errors = response.ErrorMessages });
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var response = await _mediator.Send(new DeleteEmployeeCommand
            { DeleteEmployeeDto = new DeleteEmployeeDto { Id = id } });

        if (response.Succeeded == false)
            return BadRequest(new { response.Message, Errors = response.ErrorMessages });
        return Ok(new { response.Message });
    }
}