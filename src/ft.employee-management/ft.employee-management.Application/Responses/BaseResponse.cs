namespace ft.employee_management.Application.Responses;

public class BaseResponse
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public List<string>? ErrorMessages { get; set; }
}