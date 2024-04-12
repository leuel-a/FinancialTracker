using System;
using MediatR;

using ft.transaction_management.Application.DTOs.TransactionDto;

namespace ft.transaction_management.Application.Features.Transaction.Requests.Queries;

public class GetTransactionRequest : IRequest<ReadTransactionDto>
{
    public Guid Id { get; set; }
}