namespace ft.employee_management.Application.Models;

public class DbOperationResult
{
    public bool Succeeded { get; set; } 
    public string? Message { get; set; }
    public string? ErrorMessages { get; set; }
}