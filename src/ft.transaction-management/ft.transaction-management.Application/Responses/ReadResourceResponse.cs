using System.Collections.Generic;

namespace ft.transaction_management.Application.Responses;

public class ReadResourceResponse<T>: BaseCommandResponse where T : class
{
    public T? Resource { get; set; }
    public IReadOnlyList<T>? Resources { get; set; }
}