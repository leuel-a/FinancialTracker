using System.Threading;
using System.Threading.Tasks;
using ft.transaction_management.Application.Contracts.Persistence;
using MediatR;

using ft.transaction_management.Application.Features.Transaction.Requests.Commands;

namespace ft.transaction_management.Application.Features.Transaction.Handlers.Commands;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Unit>
{
    private readonly ITransactionsRepository _transactionsRepository;

    public DeleteTransactionCommandHandler(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }
    
    public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionsRepository.GetAsync(request.Id);

        await _transactionsRepository.DeleteAsync(transaction);
        return Unit.Value;
    }
}