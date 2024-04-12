using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ft.transaction_management.Application.DTOs.TransactionDto;
using ft.transaction_management.Application.Features.Transaction.Requests.Queries;
using ft.transaction_management.Application.Persistence.Contracts;
using MediatR;

namespace ft.transaction_management.Application.Features.Transaction.Handlers.Queries;

public class GetTransactionWithDetailsRequestHandler : IRequestHandler<GetTransactionWithDetailsRequest, ReadTransactionDto>
{
    private readonly ITransactionsRepository _transactionsRepository;
    private readonly IMapper _mapper;

    public GetTransactionWithDetailsRequestHandler(ITransactionsRepository transactionsRepository, IMapper mapper)
    {
        _transactionsRepository = transactionsRepository;
        _mapper = mapper;
    }
    
    public Task<ReadTransactionDto> Handle(GetTransactionWithDetailsRequest request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}