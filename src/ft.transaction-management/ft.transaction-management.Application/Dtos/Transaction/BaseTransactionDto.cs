using System;

namespace ft.transaction_management.Application.Dtos.Transaction;

public abstract class BaseTransactionDto
{
    public string? Status { get; set; }
    public int Quantity { get; set; }
    public string? Type { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string? Region { get; set; }
    public int? NumberOfItems { get; set; }
    public string? AccountNumber { get; set; }
    public string? Description { get; set; }
    public double? CostPerItem { get; set; }
    public string? PaymentMethod { get; set; }
}