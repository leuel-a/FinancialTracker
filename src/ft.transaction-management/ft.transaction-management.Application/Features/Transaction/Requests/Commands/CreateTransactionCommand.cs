using ft.transaction_management.Application.Dtos.Transaction;
using ft.transaction_management.Application.Responses;
using MediatR;

namespace ft.transaction_management.Application.Features.Transaction.Requests.Commands;

public class CreateTransactionCommand: IRequest<CreateCommandResponse<ReadTransactionDto>>
{
    public CreateTransactionDto? CreateTransactionDto { get; set; }
}