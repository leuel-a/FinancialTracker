using ft.transaction_management.Application.Dtos.Transaction;
using ft.transaction_management.Application.Responses;
using MediatR;

namespace ft.transaction_management.Application.Features.Transaction.Requests.Commands;

public class UpdateTransactionCommand : IRequest<BaseCommandResponse>
{
    public UpdateTransactionDto? TransactionDto { get; set; }
}