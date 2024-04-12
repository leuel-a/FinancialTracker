using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ft.transaction_management.Application.DTOs.TransactionDto;
using ft.transaction_management.Application.Features.Transaction.Requests.Queries;
using ft.transaction_management.Application.Persistence.Contracts;
using MediatR;

namespace ft.transaction_management.Application.Features.Transaction.Handlers.Queries;

public class GetTransactionRequestHandler : IRequestHandler<GetTransactionRequest, ReadTransactionDto>
{
    private readonly IMapper _mapper;
    private readonly ITransactionsRepository _transactionsRepository;

    public GetTransactionRequestHandler(ITransactionsRepository transactionsRepository, IMapper mapper)
    {
        _mapper = mapper;
        _transactionsRepository = transactionsRepository;
    }
    
    public async Task<ReadTransactionDto> Handle(GetTransactionRequest request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionsRepository.GetAsync(request.Id);
        
        // Using AutoMapper to map from the Transaction to the ReadTransactionDto
        var readTransactionDto = _mapper.Map<ReadTransactionDto>(transaction);
        return readTransactionDto;
    }
}