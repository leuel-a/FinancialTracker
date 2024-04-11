using System;
using ft.transaction_management.Domain.Common;

namespace ft.transaction_management.Domain.Entities;

public class Transaction : BaseDomainEntity
{
    public string? Identifier { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime RecordedDate { get; set; }
    public DateTime DateOfTransaction { get; set; }
}