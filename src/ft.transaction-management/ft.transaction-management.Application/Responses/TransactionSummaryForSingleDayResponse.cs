using System.Collections.Generic;

namespace ft.transaction_management.Application.Responses;

public class TransactionSummaryForSingleDayResponse
{
    public bool Succeeded { get; set; }
    public string? ErrorMessage { get; set; }
    public List<string>? Errors { get; set; }
    public int TotalTransactions { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Income { get; set; }
    public decimal Expense { get; set; }
}