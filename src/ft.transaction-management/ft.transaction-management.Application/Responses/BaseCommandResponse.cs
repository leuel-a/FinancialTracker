using System.Collections.Generic;

namespace ft.transaction_management.Application.Responses;

public class BaseCommandResponse
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public List<string>? ErrorMessages { get; set; }
}