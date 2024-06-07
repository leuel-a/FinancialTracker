using MediatR;
using ft.transaction_management.Application.Responses;
using ft.transaction_management.Application.Dtos.Transaction;
using System;

namespace ft.transaction_management.Application.Features.Transaction.Requests.Queries;

public class GetAllTransactionsQuery : IRequest<PaginatedResponse<ReadTransactionDto>>
{
    public GetAllTransactionsDto? GetAllTransactionsDto { get; set; }
}