using MediatR;
using ft.transaction_management.Application.Responses;
using ft.transaction_management.Application.Dtos.Transaction;

namespace ft.transaction_management.Application.Features.Transaction.Requests.Queries;

public class GetTransactionsSummaryForSingleDayQuery : IRequest<TransactionSummaryForSingleDayResponse>
{
    public GetTransactionsSummaryDto? GetTransactionsSummaryDto { get; set; }
}