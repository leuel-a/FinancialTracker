using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ft.transaction_management.Application.Responses;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Features.Transaction.Requests.Commands;

namespace ft.transaction_management.Application.Features.Transaction.Handlers.Commands;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, BaseCommandResponse>
{
    private readonly ITransactionsRepository _transactionsRepository;

    public DeleteTransactionCommandHandler(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }

    public async Task<BaseCommandResponse> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var transaction = await _transactionsRepository.GetByIdAsync(request.Id);

        if (transaction == null)
        {
            response.Succeeded = false;
            response.Message = "Transaction not found.";
            return response;
        }

        var result = await _transactionsRepository.DeleteAsync(transaction);

        if (result.Succeeded == false)
        {
            response.Succeeded = false;
            response.Message = result.Message;
        }
        else
        {
            response.Succeeded = true;
            response.Message = "Transaction successfully deleted.";
        }

        return response;
    }
}