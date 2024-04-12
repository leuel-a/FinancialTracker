using System;
using ft.transaction_management.Application.DTOs.CommonDto;

namespace ft.transaction_management.Application.DTOs.TransactionDto;

public class UpdateTransactionDto : BaseDto, IBaseTransactionDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateOccured { get; set; }
    public string? ReceiptImgUrl { get; set; }
    public string? Description { get; set; }
}