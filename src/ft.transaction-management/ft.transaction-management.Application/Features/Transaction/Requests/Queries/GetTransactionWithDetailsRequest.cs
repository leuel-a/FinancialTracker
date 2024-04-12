using System;
using ft.transaction_management.Application.DTOs.TransactionDto;
using MediatR;

namespace ft.transaction_management.Application.Features.Transaction.Requests.Queries;

public class GetTransactionWithDetailsRequest : IRequest<ReadTransactionDto>
{
    public Guid Id { get; set; }
}