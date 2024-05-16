using System.Collections.Generic;

namespace ft.user_management.Application.Responses;

public class BaseResponse
{
    public int Id { get; set; }
    public bool? Success { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
}