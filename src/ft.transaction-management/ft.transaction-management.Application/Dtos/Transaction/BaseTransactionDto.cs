using System;

namespace ft.transaction_management.Application.Dtos.Transaction;

public abstract class BaseTransactionDto
{
    public string? Type { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public string? Description { get; set; }
}