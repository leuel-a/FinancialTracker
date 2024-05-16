using System.Collections.Generic;

namespace ft.user_management.Application.Responses;

public class ReadResourceResponse<T> : BaseResponse where T : class
{
    public T? Resource { get; set; }
    public List<T>? Resources { get; set; }
}