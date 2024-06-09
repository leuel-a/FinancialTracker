using ft.transaction_management.Application.Dtos.Transaction;
using MediatR;
using ft.transaction_management.Application.Responses;

namespace ft.transaction_management.Application.Features.Transaction.Requests.Queries;

public class GetTransactionsSummaryForSingleYearQuery : IRequest<GetTransactionsByMonthForYearResponse>
{
    public GetTransactionsSummaryDto? GetTransactionsByMonthForYearDto { get; set; }
}