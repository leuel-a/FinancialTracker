using System;

namespace ft.transaction_management.Application;

public class GetAllTransactionsDto
{
    public int? CurrentPage { get; set; }
    public int? PageSize { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public bool? CurrentYear { get; set; }
    public string? Status { get; set; }
    public string? Type { get; set; }
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
}
