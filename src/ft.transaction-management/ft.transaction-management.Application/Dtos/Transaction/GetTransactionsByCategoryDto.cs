namespace ft.transaction_management.Application.Dtos.Transaction;

public class GetTransactionsByCategoryDto
{
    public int CategoryId { get; set; }
    public int? CurrentPage { get; set; }
    public int? PageSize { get; set; }
}