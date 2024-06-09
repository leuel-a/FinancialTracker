using System.Collections.Generic;
using ft.transaction_management.Application.Models;

namespace ft.transaction_management.Application.Responses;

public class GetTransactionsSummaryForSingleMonthResponse
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
    public MonthlyTransactionSummary? Summary{ get; set; }
}