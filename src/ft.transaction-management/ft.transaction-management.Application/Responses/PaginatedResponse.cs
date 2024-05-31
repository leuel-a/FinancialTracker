using System.Collections.Generic;

namespace ft.transaction_management.Application.Responses;

public class PaginatedResponse<T>
{
    public int  CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int PreviousPage { get; set; }
    public int NextPage { get; set; }
    public IReadOnlyList<T> Data { get; set; }
}