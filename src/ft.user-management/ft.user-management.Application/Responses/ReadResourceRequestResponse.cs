using System.Collections.Generic;

namespace ft.user_management.Application.Responses;

public class ReadResourceRequestResponse<T> : BaseCommandResponse where T : class
{
    public T? Resource { get; set; }
    public List<T>? Resources { get; set; }
}