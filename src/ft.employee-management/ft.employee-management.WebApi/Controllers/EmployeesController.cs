using MediatR;
using System.Threading.Tasks;
using ft.employee_management.Application.Features.Employee.Requests.Queries;
using Microsoft.AspNetCore.Mvc;

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
}