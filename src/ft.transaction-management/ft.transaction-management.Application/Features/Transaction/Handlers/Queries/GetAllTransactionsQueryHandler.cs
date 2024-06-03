using System.Threading;
using System.Threading.Tasks;
using ft.transaction_management.Application.Dtos.Transaction;
using ft.transaction_management.Application.Features.Transaction.Requests.Queries;
using ft.transaction_management.Application.Responses;
using MediatR;

namespace ft.transaction_management.Application.Features.Transaction.Handlers.Queries;

public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, PaginatedResponse<ReadTransactionDto>>
{
    public GetAllTransactionsQueryHandler()
    {
    }
    
    public async Task<PaginatedResponse<ReadTransactionDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}