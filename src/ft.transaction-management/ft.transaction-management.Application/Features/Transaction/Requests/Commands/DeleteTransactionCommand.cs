using MediatR;
using ft.transaction_management.Application.Responses;

namespace ft.transaction_management.Application.Features.Transaction.Requests.Commands;

public class DeleteTransactionCommand : IRequest<BaseCommandResponse>
{
    public int Id { get; set; }
}