using System;
using ft.transaction_management.Domain.Common;

namespace ft.transaction_management.Domain.Entities;

public class Transaction : BaseDomainEntity
{
    public string? Status { get; set; }
    public int Quantity { get; set; }
    public string? Type { get; set; }
    public DateTime Date { get; set; }
    public string? AccountNumber { get; set; }
    public decimal Amount { get; set; }
    public string? Region { get; set; }
    public int? CategoryId { get; set; }
    public int? NumberOfItems { get; set; }
    public Category? Category { get; set; }
    public double? CostPerItem { get; set; }
    public string? Description { get; set; }
    public string? PaymentMethod { get; set; }
}