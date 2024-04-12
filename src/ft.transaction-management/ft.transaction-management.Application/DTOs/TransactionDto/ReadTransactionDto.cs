using System;
using ft.transaction_management.Application.DTOs.CommonDto;
using ft.transaction_management.Application.DTOs.TransactionDto;

namespace ft.transaction_management.Application.DTOs.TransactionDto;

public class ReadTransactionDto : BaseDto, IBaseTransactionDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime DateOccured { get; set; }
    public Guid Category { get; set; }
}