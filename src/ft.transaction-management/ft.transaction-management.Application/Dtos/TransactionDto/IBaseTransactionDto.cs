using System;

namespace ft.transaction_management.Application.DTOs.TransactionDto;

public interface IBaseTransactionDto
{
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime DateOccured { get; set; }
}