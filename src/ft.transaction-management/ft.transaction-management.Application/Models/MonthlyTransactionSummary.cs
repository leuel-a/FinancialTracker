namespace ft.transaction_management.Application.Models;

public class MonthlyTransactionSummary
{
    public int Month { get; set; }
    public decimal Income { get; set; }
    public decimal Expense { get; set; }
    public decimal Total { get; set; }
}