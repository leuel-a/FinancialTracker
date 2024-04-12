using System;
using ft.transaction_management.Application.DTOs.TransactionDto;

namespace ft.transaction_management.Application.DTOs.TransactionDto;

public class CreateTransactionDto : IBaseTransactionDto
{
    public decimal Amount { get; set; }
    public DateTime DateOccured { get; set; }
    public string? ReceiptImgUrl { get; set; }
    public string? Description { get; set; }
    
    public Guid CategoryId { get; set; }
    public Guid EmployeeId { get; set; }
}