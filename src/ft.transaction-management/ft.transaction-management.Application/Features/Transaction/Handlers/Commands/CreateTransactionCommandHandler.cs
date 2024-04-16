using System.Threading;
using System.Threading.Tasks;

using MediatR;
using AutoMapper;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.DTOs.TransactionDto;
using ft.transaction_management.Application.Features.Transaction.Requests.Commands;


namespace ft.transaction_management.Application.Features.Transaction.Handlers.Commands;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, ReadTransactionDto>
{
    private readonly IMapper _mapper;
    private readonly ITransactionsRepository _transactionsRepository;

    public CreateTransactionCommandHandler(ITransactionsRepository transactionsRepository, IMapper mapper)
    {
        _mapper = mapper;
        _transactionsRepository = transactionsRepository;
    }
    
    public async Task<ReadTransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = _mapper.Map<Domain.Entities.Transaction>(request.TransactionDto);

        // Might need to refactor this to handle the case where the transaction is not added successfully
        transaction = await _transactionsRepository.AddAsync(transaction);
        
        // Map the transaction to the ReadTransactionDto and return the result
        return _mapper.Map<ReadTransactionDto>(transaction);
    }
}