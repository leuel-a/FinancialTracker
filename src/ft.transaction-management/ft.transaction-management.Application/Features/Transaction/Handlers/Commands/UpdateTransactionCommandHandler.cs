using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Features.Transaction.Requests.Commands;
using MediatR;

namespace ft.transaction_management.Application.Features.Transaction.Handlers.Commands;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, Unit>
{
    private readonly ITransactionsRepository _transactionsRepository;
    private readonly IMapper _mapper;

    public UpdateTransactionCommandHandler(ITransactionsRepository transactionsRepository, IMapper mapper)
    {
        _mapper = mapper;
        _transactionsRepository = transactionsRepository;
    }
    
    public async Task<Unit> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = _mapper.Map<Domain.Entities.Transaction>(request.TransactionDto);

        await _transactionsRepository.UpdateAsync(transaction);
        return Unit.Value;
    }
}