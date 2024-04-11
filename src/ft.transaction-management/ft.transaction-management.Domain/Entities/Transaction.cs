using System;
using ft.transaction_management.Domain.Common;

namespace ft.transaction_management.Domain.Entities;

public class Transaction : BaseDomainEntity
{
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime DateOccured { get; set; }
    public string? ReceiptImage { get; set; }
    
    // Foreign Keys for the Navigation Properties
    public Guid EmployeeId { get; set; }
    public Guid CategoryId { get; set; }
    
    // Navigation Properties
    public Category? Category { get; set; }
    public Employee? Employee { get; set; } 
}