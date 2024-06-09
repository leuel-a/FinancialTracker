namespace ft.transaction_management.Application.Dtos.Transaction;

public class GetTransactionsSummaryDto
{
    public int Year { get; set; }
    public int? Month { get; set; }
    public int? Day { get; set; }
}