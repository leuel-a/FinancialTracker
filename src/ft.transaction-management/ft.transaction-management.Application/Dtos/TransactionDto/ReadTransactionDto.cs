using System;
using ft.transaction_management.Application.DTOs.CommonDto;
using ft.transaction_management.Application.DTOs.TransactionDto;
using ft.transaction_management.Domain.Entities;

namespace ft.transaction_management.Application.DTOs.TransactionDto;

public class ReadTransactionDto : BaseDto, IBaseTransactionDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime DateOccured { get; set; }
    public Guid CategoryId { get; set; }
    public Employee? Employee { get; set; }
    public Category? Category { get; set; }
}