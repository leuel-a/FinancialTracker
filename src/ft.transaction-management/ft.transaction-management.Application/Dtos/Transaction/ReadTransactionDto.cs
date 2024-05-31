using ft.transaction_management.Application.Dtos.Category;

namespace ft.transaction_management.Application.Dtos.Transaction;

public class ReadTransactionDto : BaseTransactionDto
{
    public int CategoryId { get; set; }
    public ReadCategoryDto? Category { get; set; }
}