namespace ft.employee_management.Application.Responses;

public class ReadResourceResponse<T> : BaseResponse where T : class
{
    public T? Resource { get; set; }
    public IReadOnlyList<T>? Resources { get; set; }
    public int? PageSize { get; set; }
    public int? TotalPages { get; set; }
    public int? CurrentPage { get; set; }
    public int? NextPage { get; set; }
    public int? PreviousPage { get; set; }
    public int? TotalRecords { get; set; }
}