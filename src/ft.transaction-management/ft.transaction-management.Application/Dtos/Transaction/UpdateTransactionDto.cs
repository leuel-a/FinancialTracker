namespace ft.transaction_management.Application.Dtos.Transaction;

public class UpdateTransactionDto : BaseTransactionDto
{
    public int Id { get; set; }
    public int? CategoryId { get; set; }
}