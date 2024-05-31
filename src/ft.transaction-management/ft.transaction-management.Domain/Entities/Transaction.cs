using System;
using ft.transaction_management.Domain.Common;

namespace ft.transaction_management.Domain.Entities;

public class Transaction : BaseDomainEntity
{
    public string? Type { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    // Navigation property for a transaction
    public Category? Category { get; set; }
}