using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Dtos.Transaction;
using ft.transaction_management.Application.Features.Transaction.Requests.Queries;
using ft.transaction_management.Application.Responses;
using MediatR;

namespace ft.transaction_management.Application.Features.Transaction.Handlers.Queries;

public class
    GetSingleTransactionQueryHandler : IRequestHandler<GetSingleTransactionQuery,
    ReadResourceResponse<ReadTransactionDto>>
{
    private readonly IMapper _mapper;
    private readonly ITransactionsRepository _transactionsRepository;

    public GetSingleTransactionQueryHandler(IMapper mapper, ITransactionsRepository transactionsRepository)
    {
        _mapper = mapper;
        _transactionsRepository = transactionsRepository;
    }
    
    public async Task<ReadResourceResponse<ReadTransactionDto>> Handle(GetSingleTransactionQuery query, CancellationToken cancellationToken)
    {
        var response = new ReadResourceResponse<ReadTransactionDto>();
        var transaction = await _transactionsRepository.GetByIdAsync(query.Id);

        if (transaction == null)
        {
            response.Succeeded = false;
            response.Message = "Transaction not found.";
            return response;
        }

        response.Succeeded = true;
        response.Message = "Transaction has been found.";
        response.Resource = _mapper.Map<ReadTransactionDto>(transaction);
        return response;
    }
}