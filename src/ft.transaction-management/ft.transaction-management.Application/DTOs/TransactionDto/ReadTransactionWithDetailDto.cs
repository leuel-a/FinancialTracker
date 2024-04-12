using System;
using ft.transaction_management.Domain.Entities;
using ft.transaction_management.Application.DTOs.CommonDto;

namespace ft.transaction_management.Application.DTOs.TransactionDto;

public class ReadTransactionWithDetailDto : BaseDto, IBaseTransactionDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime DateOccured { get; set; }
    public Employee? Employee { get; set; }
}