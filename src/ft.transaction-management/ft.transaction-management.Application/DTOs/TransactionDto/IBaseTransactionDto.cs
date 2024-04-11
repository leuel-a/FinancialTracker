using System;

namespace ft.transaction_management.Application.DTOs.TransactionDto;

public interface IBaseTransactionDto
{
    public decimal Amount { get; set; }
}