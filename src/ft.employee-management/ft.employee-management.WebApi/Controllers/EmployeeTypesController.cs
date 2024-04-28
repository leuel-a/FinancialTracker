using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ft.employee_management.Application.Dtos.EmployeeTypeDto;
using ft.employee_management.Application.Features.EmployeeType.Requests.Queries;
using ft.employee_management.Application.Features.EmployeeType.Requests.Commands;

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

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEmployeeType(int id, [FromBody] EmployeeTypeDto employeeTypeDto)
    {
        await _mediator.Send(new UpdateEmployeeTypeCommand() {EmployeeTypeDto = employeeTypeDto});
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployeeType(int id)
    {
        // Get the EmployeeType from the database
        var employeeType = await _mediator.Send(new GetEmployeeTypeRequest() { Id = id });
        employeeType.IsActive = false; // Update the IsActive attribute for an employee type
        
        // Update the EmployeeType in the database
        await _mediator.Send(new UpdateEmployeeTypeCommand() { EmployeeTypeDto = employeeType });
        return NoContent();
    }
}