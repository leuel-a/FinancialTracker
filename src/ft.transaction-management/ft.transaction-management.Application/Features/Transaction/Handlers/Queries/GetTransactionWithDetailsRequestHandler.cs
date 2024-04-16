using System.Threading;
using System.Threading.Tasks;

using MediatR;
using AutoMapper;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.DTOs.TransactionDto;
using ft.transaction_management.Application.Features.Transaction.Requests.Queries;

namespace ft.transaction_management.Application.Features.Transaction.Handlers.Queries;

public class GetTransactionWithDetailsRequestHandler : IRequestHandler<GetTransactionWithDetailsRequest, ReadTransactionDto>
{
    private readonly IMapper _mapper;
    private readonly ITransactionsRepository _transactionsRepository;

    public GetTransactionWithDetailsRequestHandler(ITransactionsRepository transactionsRepository, IMapper mapper)
    {
        _transactionsRepository = transactionsRepository;
        _mapper = mapper;
    }
    
    public async Task<ReadTransactionDto> Handle(GetTransactionWithDetailsRequest request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionsRepository.GetTransactionWithDetails(request.Id);
    
        // Use AutoMapper to map from the Transaction to the ReadTransactionDto
        var readTransactionDto = _mapper.Map<ReadTransactionDto>(transaction);
        return readTransactionDto;
    }
}