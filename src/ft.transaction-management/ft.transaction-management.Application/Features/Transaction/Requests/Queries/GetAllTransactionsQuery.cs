using ft.transaction_management.Application.Dtos.Transaction;
using ft.transaction_management.Application.Responses;
using MediatR;

namespace ft.transaction_management.Application.Features.Transaction.Requests.Queries;

public class GetAllTransactionsQuery : IRequest<PaginatedResponse<ReadTransactionDto>>
{
}