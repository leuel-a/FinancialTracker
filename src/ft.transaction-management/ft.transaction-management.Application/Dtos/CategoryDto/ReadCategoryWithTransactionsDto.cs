using System;
using System.Collections.Generic;
using ft.transaction_management.Application.DTOs.CommonDto;
using ft.transaction_management.Application.DTOs.TransactionDto;

namespace ft.transaction_management.Application.DTOs.CategoryDto;

public class ReadCategoryWithTransactionsDto : BaseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<ReadTransactionDto> Transactions { get; set; }
}