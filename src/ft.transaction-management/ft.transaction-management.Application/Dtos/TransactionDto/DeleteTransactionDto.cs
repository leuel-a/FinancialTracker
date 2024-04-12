using System;
using ft.transaction_management.Application.DTOs.CommonDto;

namespace ft.transaction_management.Application.DTOs.TransactionDto;

public class DeleteTransactionDto : BaseDto
{
    public Guid Id { get; set; }
}