using System.Collections.Generic;

namespace ft.transaction_management.Application.Responses;

public class ReadResourceResponse<T> where T : class
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public T? Resource { get; set; }
    public IReadOnlyList<T>? Resources { get; set; }
}