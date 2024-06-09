using MediatR;
using ft.transaction_management.Application.Responses;
using ft.transaction_management.Application.Dtos.Transaction;

namespace ft.transaction_management.Application.Features.Transaction.Requests.Queries;

public class GetTransactionsSummaryForSingleMonthQuery : IRequest<GetTransactionsSummaryForSingleMonthResponse>
{
    public GetTransactionsSummaryDto GetTransactionsSummaryDto { get; set; }
}